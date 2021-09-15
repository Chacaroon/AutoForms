﻿namespace FormBuilder.UnitTests
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

            var arrayPropertyNodes = FindNode(node, nameof(ComplexType.ArrayProperty), NodeType.Array);
            var arrayPropertyValues = ((FormArray)arrayPropertyNodes).Nodes.Select(x => ((FormControl)x).Value);

            // Arrange
            Assert.NotNull(node);

            Assert.AreEqual("value", ((FormControl)FindNode(node, nameof(ComplexType.StringProperty), NodeType.Control)).Value);
            Assert.That(arrayPropertyValues, Is.EquivalentTo(new[] { 0 }));
        }

        [Test]
        public void Resolve_PrimitiveTypeModelArray_ReturnsFromArrayWithFormControlsAndPopulatedValues()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();
            var model = Enumerable.Range(0, 3);

            // Act
            var node = formResolver.Resolve(model) as FormArray;

            // Arrange
            Assert.NotNull(node);

            Assert.AreEqual(3, node.Nodes.Count());
            Assert.That(node.Nodes.Select(x => ((FormControl)x).Value).Cast<int>(), Is.EquivalentTo(Enumerable.Range(0, 3)));
        }

        [Test]
        public void Resolve_ComplexTypeModelArray_ReturnsFromArrayWithFormGroupsAndPopulatedValues()
        {
            // Assert
            var formResolver = _serviceProvider.GetRequiredService<FormResolver>();
            var model = new[]
            {
                GetTestModel(1),
                GetTestModel(2)
            };

            // Act
            var node = formResolver.Resolve(model) as FormArray;

            var stringPropertyNodes = node!.Nodes
                .Select(x => FindNode(x as FormGroup, nameof(ComplexType.StringProperty), NodeType.Control));
            var stringPropertyNodeValues = stringPropertyNodes
                .Select(x => ((FormControl)x).Value);

            // Arrange
            Assert.NotNull(node);

            Assert.AreEqual(2, node.Nodes.Count());
            Assert.That(stringPropertyNodeValues, Is.EquivalentTo(new[] { "value1", "value2" }));
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

        private Node FindNode(FormGroup node, string nodeName, NodeType nodeType)
        {
            return node.Nodes.FirstOrDefault(x => x.Name == nodeName && x.Type == nodeType);
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
