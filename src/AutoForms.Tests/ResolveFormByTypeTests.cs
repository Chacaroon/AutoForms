using System;
using System.Collections.Generic;
using AutoForms.Exceptions;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AutoForms.Tests;

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
    public void Resolve_ComplexTypeAsGenericParameter_ReturnsFormGroupWithItsChildControls()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexType>();

        // Act
        var formGroup = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(formGroup);

        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.StringProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.ArrayProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexValueTypeAsGenericParameter_ReturnsFormGroupWithItsChildControls()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexStruct>();

        // Act
        var formGroup = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(formGroup);

        Assert.NotNull(FindControl(formGroup, nameof(ComplexStruct.StringProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexStruct.ArrayProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexStruct.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexTypeAsMethodParameter_ReturnsFormGroupWithItsChildControls()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder(typeof(ComplexType));

        // Act
        var formGroup = formBuilder.Build() as FormGroup;

        // Arrange
        Assert.NotNull(formGroup);

        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.StringProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.ArrayProperty)));
        Assert.NotNull(FindControl(formGroup, nameof(ComplexType.ComplexTypeProperty)));
    }

    [Test]
    public void Resolve_ComplexTypeArrayAsGenericParameter_ReturnsFromArrayWithoutChildControls()
    {
        // Assert
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<ComplexType[]>();

        // Act
        var formArray = formBuilder.Build() as FormArray;

        // Arrange
        Assert.NotNull(formArray);

        Assert.IsEmpty(formArray.Controls);
    }
    
    [TestCase(typeof(FirstClass), "Circular dependency: FirstClass->SecondClass->ThirdClass->FirstClass")]
    [TestCase(typeof(FirstClassWithCollection), "Circular dependency: FirstClassWithCollection->IEnumerable`1->SecondClassWithCollection->IEnumerable`1->FirstClassWithCollection")]
    public void Resolve_ClassWithCircularDependency_ThrowException(Type type, string expectedMessage)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(new(type));

        // Assert
        var exception = Assert.Throws<CircularDependencyException>(() => strategy.Process(new()))!;
        Assert.AreEqual(expectedMessage, exception.Message);
    }


    #region Helpers

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
