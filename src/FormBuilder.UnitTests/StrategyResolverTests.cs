namespace FormBuilder.UnitTests
{
    using System;
    using System.Collections.Generic;
    using FormBuilder.Extensions;
    using FormBuilder.Strategies;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    internal class StrategyResolverTests
    {
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection()
                .AddFormBuilder();

            _serviceProvider = services.BuildServiceProvider();
        }

        [TestCase(typeof(string))]
        [TestCase(typeof(int))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(TestEnum))]
        public void Resolve_PrimitiveType_ReturnsFormControlStrategy(Type type)
        {
            // Arrange
            var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

            // Act
            var strategy = strategyResolver.Resolve(type);

            // Assert
            Assert.AreEqual(typeof(FormControlStrategy), strategy.GetType());
        }

        [TestCase(typeof(int[]))]
        [TestCase(typeof(TestClass[]))]
        [TestCase(typeof(IEnumerable<int>))]
        [TestCase(typeof(List<int>))]
        public void Resolve_CollectionType_ReturnsFormArrayStrategy(Type type)
        {
            // Arrange
            var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

            // Act
            var strategy = strategyResolver.Resolve(type);

            // Assert
            Assert.AreEqual(typeof(FormArrayStrategy), strategy.GetType());
        }

        [TestCase(typeof(TestClass))]
        public void Resolve_ComplexType_ReturnsFormGroupStrategy(Type type)
        {
            // Arrange
            var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

            // Act
            var strategy = strategyResolver.Resolve(type);

            // Assert
            Assert.AreEqual(typeof(FormGroupStrategy), strategy.GetType());
        }

        #region TestData

        private enum TestEnum { }

        private class TestClass { }

        #endregion
    }
}
