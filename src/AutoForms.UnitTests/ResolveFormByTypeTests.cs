namespace AutoForms.UnitTests
{
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using AutoForms.Extensions;
    using AutoForms.Models;

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
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();

            // Act
            var node = formResolver.Resolve<ComplexType>() as FormGroup;

            // Arrange
            Assert.NotNull(node);

            Assert.NotNull(FindNode(node, nameof(ComplexType.StringProperty)));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ArrayProperty)));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ComplexTypeProperty)));
        }

        [Test]
        public void Resolve_ComplexTypeAsMethodParameter_ReturnsFormGroupWithItsChildNodes()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();

            // Act
            var node = formResolver.Resolve(typeof(ComplexType)) as FormGroup;

            // Arrange
            Assert.NotNull(node);

            Assert.NotNull(FindNode(node, nameof(ComplexType.StringProperty)));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ArrayProperty)));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ComplexTypeProperty)));
        }

        [Test]
        public void Resolve_ComplexTypeArrayAsGenericParameter_ReturnsFromArrayWithoutChildNodes()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();

            // Act
            var node = formResolver.Resolve<ComplexType[]>() as FormArray;

            // Arrange
            Assert.NotNull(node);

            Assert.IsEmpty(node.Nodes);
        }

        #region Helpers

        private Node FindNode(FormGroup node, string nodeName)
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

        private class NestedComplexType { }

        #endregion
    }
}
