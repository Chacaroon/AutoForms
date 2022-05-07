using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoForms.UnitTests")]
namespace AutoForms
{
using AutoForms.Enums;
    using AutoForms.FormResolverStrategies;
    using AutoForms.Models;
    using System;
    using System.Reflection;

    public class FormResolver
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

        public Node Resolve(Type modelType, object value)
        {
            var node = _strategyResolver
                .Resolve(modelType)
                .EnhanceWithValue(value)
                .EnhanceWithValidators(modelType)
                .Process(modelType);

            return node;
        }

        internal Node Resolve(PropertyInfo propertyInfo, object value)
        {
            return _strategyResolver.Resolve(propertyInfo)
                .EnhanceWithValue(value)
                .EnhanceWithValidators(propertyInfo)
                .Process(propertyInfo.PropertyType);
        }
    }
}
