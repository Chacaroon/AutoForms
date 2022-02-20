namespace AutoForms.FormResolverStrategies.Strategies
{
    using AutoForms.Helpers;
    using AutoForms.Models;
    using AutoForms.Options;
    using System;
    using System.Linq;
    using System.Reflection;

    internal class FormGroupStrategy : BaseStrategy
    {
        private readonly StrategyResolver _strategyResolver;

        public FormGroupStrategy(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
        {
            return PropertyFormControlTypeResolver.IsFormGroup(modelType, options);
        }

        internal override Node Process(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var nodes = properties.ToDictionary(x => x.Name, x =>
                _strategyResolver
                    .Resolve(x)
                    .EnhanceWithValue(GetPropertyValue(x, Value))
                    .Process(x.PropertyType));

            return new FormGroup
            {
                Nodes = nodes
            };
        }

        private object GetPropertyValue(PropertyInfo propertyInfo, object value)
        {
            if (value != null)
            {
                return propertyInfo.GetValue(value);
            }

            if (propertyInfo.PropertyType.IsValueType)
            {
                return Activator.CreateInstance(propertyInfo.PropertyType);
            }

            return null;
        }
    }
}
