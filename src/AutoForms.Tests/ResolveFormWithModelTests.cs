using System;
using System.Collections.Generic;
using System.Linq;
using AutoForms.Enums;
using AutoForms.Extensions;
using AutoForms.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AutoForms.Tests;

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
        var formGroup = formResolver.CreateFormBuilder(model).Build() as FormGroup;

        // Arrange
        var arrayPropertyControls = FindControl(formGroup, nameof(ComplexType.ArrayProperty));
        var arrayPropertyValues = ((FormArray)arrayPropertyControls).Controls.Select(x => ((FormControl)x).Value);

        Assert.NotNull(formGroup);

        Assert.AreEqual("value", ((FormControl)FindControl(formGroup, nameof(ComplexType.StringProperty))).Value);
        Assert.That(arrayPropertyValues, Is.EquivalentTo(new[] { 0 }));
    }

    [Test]
    public void Resolve_PrimitiveTypeModelArray_ReturnsFromArrayWithFormControlsAndPopulatedValues()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = Enumerable.Range(0, 3);

        // Act
        var formArray = formResolver.CreateFormBuilder(model).Build() as FormArray;

        // Arrange
        Assert.NotNull(formArray);

        Assert.AreEqual(3, formArray.Controls.Count());
        Assert.That(formArray.Controls.Select(x => ((FormControl)x).Value).Cast<int>(), Is.EquivalentTo(Enumerable.Range(0, 3)));
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
        var formArray = formResolver.CreateFormBuilder(model).Build() as FormArray;

        // Arrange
        var stringPropertyControls = formArray!.Controls
            .Select(x => FindControl(x as FormGroup, nameof(ComplexType.StringProperty)));
        var stringPropertyControlValues = stringPropertyControls
            .Select(x => ((FormControl)x).Value);

        Assert.NotNull(formArray);

        Assert.AreEqual(2, formArray.Controls.Count());
        Assert.That(stringPropertyControlValues, Is.EquivalentTo(new[] { "value1", "value2" }));
    }

    [Test]
    public void Resolve_Dictionary_ReturnsFromGroupWithPopulatedValues()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = new Dictionary<string, int>
        {
            { "first", 1 },
            { "second", 2 }
        };

        // Act
        var formGroup = formResolver.CreateFormBuilder(model).Build() as FormGroup;

        // Arrange
        Assert.NotNull(formGroup);

        Assert.AreEqual(ControlType.Group, formGroup.Type);
        Assert.AreEqual(2, formGroup.Controls.Count);
        Assert.That(formGroup.Controls.Keys, Is.EquivalentTo(new[] { "first", "second" }));
        Assert.AreEqual((formGroup.Controls["first"] as FormControl)!.Value, 1);
        Assert.AreEqual((formGroup.Controls["second"] as FormControl)!.Value, 2);
    }

    [Test]
    public void Resolve_TypeMismatch_ThrowArgumentException()
    {
        // Assert
        var formResolver = _serviceProvider.GetRequiredService<FormBuilderFactory>();
        var model = GetTestModel();
        var formBuilder = formResolver.CreateFormBuilder<string>();

        // Act & Arrange
        Assert.Throws<ArgumentException>(() => formBuilder.EnhanceWithValue(model));
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

    private AbstractControl FindControl(FormGroup formGroup, string formControlName)
    {
        return formGroup.Controls.GetValueOrDefault(formControlName.FirstCharToLowerCase());
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