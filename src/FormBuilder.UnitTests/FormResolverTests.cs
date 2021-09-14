namespace FormBuilder.UnitTests
{
    using System;
    using System.Linq;
    using FormBuilder.Enums;
    using FormBuilder.Extensions;
    using FormBuilder.Models;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    internal class FormResolverTests
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
        public void Resolve_ComplexType_ReturnsFormGroupWithItsChildNodes()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();

            // Act
            var node = formResolver.Resolve<ComplexType>() as FormGroup;

            // Arrange
            Assert.NotNull(node);

            Assert.NotNull(FindNode(node, nameof(ComplexType.StringProperty), NodeType.Control));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ArrayProperty), NodeType.Array));
            Assert.NotNull(FindNode(node, nameof(ComplexType.ComplexTypeProperty), NodeType.Group));
        }

        [Test]
        public void Resolve_ComplexType_ReturnsFromArrayWithoutChildNodes()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();

            // Act
            var node = formResolver.Resolve<ComplexType[]>() as FormArray;

            // Arrange
            Assert.NotNull(node);

            Assert.IsEmpty(node.Nodes);
        }

        private Node FindNode(FormGroup node, string nodeName, NodeType nodeType)
        {
            return node.Nodes.FirstOrDefault(x => x.Name == nodeName && x.Type == nodeType);
        }

        class ComplexType
        {
            public string StringProperty { get; set; }

            public int[] ArrayProperty { get; set; }

            public NestedComplexType ComplexTypeProperty { get; set; }
        }

        class NestedComplexType { }
    }
}
