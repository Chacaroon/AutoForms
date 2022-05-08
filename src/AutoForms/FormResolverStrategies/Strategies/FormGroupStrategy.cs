namespace AutoForms.FormResolverStrategies.Strategies
{
    using AutoForms.Helpers;
    using AutoForms.Models;
    using AutoForms.Options;
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoForms.Extensions;

    internal class FormGroupStrategy : BaseStrategy
    {
        private readonly FormResolver _formResolver;

        public FormGroupStrategy(FormResolver formResolver)
        {
            _formResolver = formResolver;
        }

        internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
        {
            return PropertyFormControlTypeResolver.IsFormGroup(modelType, options);
        }

        internal override Node Process(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var nodes = properties.ToDictionary(x => x.Name.FirstCharToLowerCase(), x =>
                _formResolver.Resolve(x, GetPropertyValue(x, Value)));

            return new FormGroup
            {
                Nodes = nodes
            };
        }

        private object GetPropertyValue(PropertyInfo propertyInfo, object value)
        {
            if (value == null)
            {
                return null;
            }

            return propertyInfo.PropertyType.IsValueType 
                ? Activator.CreateInstance(propertyInfo.PropertyType)
                : propertyInfo.GetValue(value);
        }
    }
}
