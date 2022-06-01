using System;
using System.Collections;
using System.Collections.Generic;
using AutoForms.Extensions;
using AutoForms.FormBuilderStrategies;
using AutoForms.FormBuilderStrategies.Strategies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AutoForms.Tests;

[TestFixture]
internal class StrategyResolverTests
{
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection()
            .AddAutoForms();

        _serviceProvider = services.BuildServiceProvider();
    }

    [TestCase(typeof(object))]
    [TestCase(typeof(string))]
    [TestCase(typeof(int))]
    [TestCase(typeof(DateTime))]
    [TestCase(typeof(TestEnum))]
    public void Resolve_PrimitiveType_ReturnsFormControlStrategy(Type type)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(type, new());

        // Assert
        Assert.IsInstanceOf<FormControlStrategy>(strategy);
    }

    [TestCase(typeof(IEnumerable))]
    [TestCase(typeof(object[]))]
    [TestCase(typeof(int[]))]
    [TestCase(typeof(TestClass[]))]
    [TestCase(typeof(IEnumerable<int>))]
    [TestCase(typeof(List<int>))]
    public void Resolve_CollectionType_ReturnsFormArrayStrategy(Type type)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(type, new());

        // Assert
        Assert.IsInstanceOf<FormArrayStrategy>(strategy);
    }

    [TestCase(typeof(TestClass))]
    public void Resolve_ComplexType_ReturnsFormGroupStrategy(Type type)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(type, new());

        // Assert
        Assert.IsInstanceOf<FormGroupStrategy>(strategy);
    }
    
    [TestCase(typeof(Dictionary<string, int>))]
    public void Resolve_ComplexType_ReturnsDictionaryFormGroupStrategy(Type type)
    {
        // Arrange
        var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

        // Act
        var strategy = strategyResolver.Resolve(type, new());

        // Assert
        Assert.IsInstanceOf<DictionaryFormGroupStrategy>(strategy);
    }

    #region TestData

    private enum TestEnum { }

    private class TestClass { }

    #endregion
}