using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormBuilder.UnitTests")]
namespace FormBuilder
{
    using System;
    using FormBuilder.Models;
    using FormBuilder.Strategies;

    internal class FormResolver
    {
        private readonly StrategyResolver _strategyResolver;

        public FormResolver(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        public const string RootNodeName = "root";

        public Node Resolve<TModel>()
        {
            return Resolve(typeof(TModel), null);
        }

        public Node Resolve<TModel>(TModel model)
        {
            return Resolve(typeof(TModel), model);
        }

        public Node Resolve(Type modelType)
        {
            return Resolve(modelType, null);
        }

        private Node Resolve(Type modelType, object value)
        {
            var node = _strategyResolver
                .Resolve(modelType)
                .EnhanceWithValue(value)
                .Process(RootNodeName, modelType);

            return node;
        }
    }
}
