using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoForms.Enums;
using AutoForms.Extensions;
using AutoForms.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AutoForms.UnitTests;

public class ValidatorResolverTests
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
    public void Resolve_StringPropertyWithValidators_ReturnsNodeWithValidators()
    {
        // Arrange
        var formBuilder = _serviceProvider.GetRequiredService<FormBuilderFactory>()
            .CreateFormBuilder<TestClass>()
            .EnhanceWithValidators();

        // Act
        var node = (formBuilder.Build() as FormGroup)!;

        // Assert
        var testPropertyNode = node.Nodes.First(x => x.Key == nameof(TestClass.TestProperty).FirstCharToLowerCase()).Value;
        var requiredPropertyNode = node.Nodes.First(x => x.Key == nameof(TestClass.RequiredProperty).FirstCharToLowerCase()).Value;

        Assert.AreEqual(2, testPropertyNode.Validators.Length);
        Assert.NotNull(testPropertyNode.Validators.FirstOrDefault(x => x.Type == ValidatorType.MinLength));
        Assert.NotNull(testPropertyNode.Validators.FirstOrDefault(x => x.Type == ValidatorType.MaxLength));

        Assert.NotNull(requiredPropertyNode.Validators.FirstOrDefault(x => x.Type == ValidatorType.Required));
    }

    #region TestData

    private class TestClass
    {
        [MinLength(5)]
        [MaxLength(10)]
        public string TestProperty { get; set; }

        [Required]
        public string RequiredProperty { get; set; }
    }

    #endregion
}
