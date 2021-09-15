namespace FormBuilder.UnitTests
{
    using System;
    using System.Linq;
    using FormBuilder.Enums;
    using FormBuilder.Extensions;
    using FormBuilder.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    internal class ResolveFormWithModelTests
    {
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection()
                .AddFormBuilder();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void Resolve_ComplexTypeModel_ReturnsFromGroupWithPopulatedValues()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();
            var model = GetTestModel();

            // Act
            var node = formResolver.Resolve(model) as FormGroup;

            // Arrange
            Assert.NotNull(node);

            Assert.AreEqual("value", FindNode(node, nameof(ComplexType.StringProperty), NodeType.Control).Value);
        }

        private ComplexType GetTestModel()
        {
            return new ComplexType
            {
                StringProperty = "value",
                ArrayProperty = new[] { 0 }
            };
        }

        private Node FindNode(FormGroup node, string nodeName, NodeType nodeType)
        {
            return node.Nodes.FirstOrDefault(x => x.Name == nodeName && x.Type == nodeType);
        }

        private class ComplexType
        {
            public string StringProperty { get; set; }

            public int[] ArrayProperty { get; set; }

            public NestedComplexType ComplexTypeProperty { get; set; }
        }

        private class NestedComplexType { }
    }
}
