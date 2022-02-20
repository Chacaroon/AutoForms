using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoForms.UnitTests")]
namespace AutoForms
{
    using AutoForms.FormResolverStrategies;
    using AutoForms.Models;
    using System;

    internal class FormResolver
    {
        private readonly StrategyResolver _strategyResolver;

        public FormResolver(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

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
                .Process(modelType);

            return node;
        }
    }
}
