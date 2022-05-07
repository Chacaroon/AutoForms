using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace AutoForms.UnitTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoForms.Extensions;
using AutoForms.FormResolverStrategies.Strategies;
using AutoForms.FormResolverStrategies;
using AutoForms.Models;
using AutoForms.Enums;

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
            var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

            // Act
            var node = strategyResolver.Resolve(typeof(TestClass)).Process(typeof(TestClass)) as FormGroup;

            var testPropertyNode = node.Nodes.First(x => x.Key == nameof(TestClass.TestProperty)).Value;
            var requiredPropertyNode = node.Nodes.First(x => x.Key == nameof(TestClass.RequiredProperty)).Value;

            // Assert
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
}
