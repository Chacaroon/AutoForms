namespace FormBuilder.UnitTests
{
    using System;
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
        public void Resolve_PrimitiveType_FormControlStrategy(Type type)
        {
            // Arrange
            var strategyResolver = _serviceProvider.GetRequiredService<StrategyResolver>();

            // Act
            var strategy = strategyResolver.Resolve(type);

            // Assert
            Assert.AreEqual(typeof(FormControlStrategy), strategy.GetType());
        }

        private enum TestEnum { }
    }
}
