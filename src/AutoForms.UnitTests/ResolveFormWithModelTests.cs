namespace AutoForms.UnitTests;

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoForms.Extensions;
using AutoForms.Models;

internal class ResolveFormWithModelTests
{
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection()
            .AddAutoForms();

        _serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void Resolve_ComplexTypeModel_ReturnsFromGroupWithPopulatedValues()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = GetTestModel();

        // Act
        var node = formResolver.CreateFormBuilder(model).Build() as FormGroup;

        var arrayPropertyNodes = FindNode(node, nameof(ComplexType.ArrayProperty).FirstCharToLowerCase());
        var arrayPropertyValues = ((FormArray)arrayPropertyNodes).Nodes.Select(x => ((FormControl)x).Value);

        // Arrange
        Assert.NotNull(node);

        Assert.AreEqual("value", ((FormControl)FindNode(node, nameof(ComplexType.StringProperty).FirstCharToLowerCase())).Value);
        Assert.That(arrayPropertyValues, Is.EquivalentTo(new[] { 0 }));
    }

    [Test]
    public void Resolve_PrimitiveTypeModelArray_ReturnsFromArrayWithFormControlsAndPopulatedValues()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = Enumerable.Range(0, 3);

        // Act
        var node = formResolver.CreateFormBuilder(model).Build() as FormArray;

        // Arrange
        Assert.NotNull(node);

        Assert.AreEqual(3, node.Nodes.Count());
        Assert.That(node.Nodes.Select(x => ((FormControl)x).Value).Cast<int>(), Is.EquivalentTo(Enumerable.Range(0, 3)));
    }

    [Test]
    public void Resolve_ComplexTypeModelArray_ReturnsFromArrayWithFormGroupsAndPopulatedValues()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = new[]
        {
            GetTestModel(1),
            GetTestModel(2)
        };

        // Act
        var node = formResolver.CreateFormBuilder(model).Build() as FormArray;

        var stringPropertyNodes = node!.Nodes
            .Select(x => FindNode(x as FormGroup, nameof(ComplexType.StringProperty).FirstCharToLowerCase()));
        var stringPropertyNodeValues = stringPropertyNodes
            .Select(x => ((FormControl)x).Value);

        // Arrange
        Assert.NotNull(node);

        Assert.AreEqual(2, node.Nodes.Count());
        Assert.That(stringPropertyNodeValues, Is.EquivalentTo(new[] { "value1", "value2" }));
    }

    #region Helpers

    private ComplexType GetTestModel(int? count = null)
    {
        return new ComplexType
        {
            StringProperty = $"value{count}",
            ArrayProperty = new[] { 0 }
        };
    }

    private Node FindNode(FormGroup node, string nodeName)
    {
        return node.Nodes.GetValueOrDefault(nodeName);
    }

    #endregion

    #region TestData

    private class ComplexType
    {
        public string StringProperty { get; set; }

        public int[] ArrayProperty { get; set; }

        public NestedComplexType ComplexTypeProperty { get; set; }
    }

    private class NestedComplexType { }

    #endregion
}