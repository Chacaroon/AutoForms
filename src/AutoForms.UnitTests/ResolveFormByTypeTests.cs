namespace AutoForms.UnitTests;

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using AutoForms.Comparers;
using AutoForms.Exceptions;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.FormBuilderStrategies;

[TestFixture]
internal class ResolveFormByTypeTests
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
    public void Resolve_ComplexTypeAsGenericParameter_ReturnsFormGroupWithItsChildNodes()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexType>();

        // Act
        var node = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(node);

        Assert.NotNull(FindNode<FormControl>(node, nameof(ComplexType.StringProperty)));
        Assert.NotNull(FindNode<FormArray>(node, nameof(ComplexType.ArrayProperty)));
        Assert.NotNull(FindNode<FormGroup>(node, nameof(ComplexType.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexValueTypeAsGenericParameter_ReturnsFormGroupWithItsChildNodes()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexStruct>();

        // Act
        var node = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(node);

        Assert.NotNull(FindNode<FormControl>(node, nameof(ComplexStruct.StringProperty)));
        Assert.NotNull(FindNode<FormArray>(node, nameof(ComplexStruct.ArrayProperty)));
        Assert.NotNull(FindNode<FormGroup>(node, nameof(ComplexStruct.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexTypeAsMethodParameter_ReturnsFormGroupWithItsChildNodes()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder(typeof(ComplexType));

        // Act
        var node = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(node);

        Assert.NotNull(FindNode<FormControl>(node, nameof(ComplexType.StringProperty)));
        Assert.NotNull(FindNode<FormArray>(node, nameof(ComplexType.ArrayProperty)));
        Assert.NotNull(FindNode<FormGroup>(node, nameof(ComplexType.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexTypeArrayAsGenericParameter_ReturnsFromArrayWithoutChildNodes()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexType[]>();

        // Act
        var node = formBuilder.Build() as FormArray;

        // Arrange
        Assert.NotNull(node);

        Assert.IsEmpty(node.Nodes);
    }
    
    [TestCase(typeof(FirstClass), "Circular dependency: FirstClass->SecondClass->ThirdClass->FirstClass")]
    [TestCase(typeof(FirstClassWithCollection), "Circular dependency: FirstClassWithCollection->IEnumerable`1->SecondClassWithCollection->IEnumerable`1->FirstClassWithCollection")]
    public void Resolve_ClassWithCircularDependency_ThrowException(Type type, string expectedMessage)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(type);

        // Assert
        var exception = Assert.Throws<CircularDependencyException>(() => strategy.Process(type, new(new TypeEqualityComparer())));
        Assert.AreEqual(expectedMessage, exception.Message);
    }


    #region Helpers

    private Node FindNode<T>(FormGroup node, string nodeName) where T : Node
    {
        return node.Nodes.GetValueOrDefault(nodeName.FirstCharToLowerCase());
    }

    #endregion

    #region TestData

    private class ComplexType
    {
        public string StringProperty { get; set; }

        public int[] ArrayProperty { get; set; }

        public NestedComplexType ComplexTypeProperty { get; set; }
    }

    private struct ComplexStruct
    {
        public string StringProperty { get; set; }

        public int[] ArrayProperty { get; set; }

        public NestedComplexStruct ComplexTypeProperty { get; set; }
    }

    private struct NestedComplexStruct { }

    private class NestedComplexType { }

    private class FirstClass
    {
        public SecondClass SecondClass { get; set; }
    }

    private class SecondClass
    {
        public ThirdClass ThirdClass { get; set; }
    }

    private class ThirdClass
    {
        public FirstClass FirstClass { get; set; }
    }

    private class FirstClassWithCollection
    {
        public IEnumerable<SecondClassWithCollection> SecondClassWithCollections { get; set; }
    }

    private class SecondClassWithCollection
    {
        public IEnumerable<FirstClassWithCollection> FirstClassWithCollections { get; set; }
    }

    #endregion
}
