namespace AutoForms.FormResolverStrategies.Strategies
{
    using AutoForms.Models;
    using AutoForms.Options;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using AutoForms.Enums;
    using Validator = Models.Validator;

    public abstract class BaseStrategy
    {
        private protected object Value { get; private set; }

        private protected Validator[] Validators { get; private set; }

        internal abstract bool IsStrategyApplicable(Type modelType, StrategyOptions options);

        internal abstract Node Process(Type type);

        internal BaseStrategy EnhanceWithValue(object value)
        {
            Value = value;

            return this;
        }

        internal BaseStrategy EnhanceWithValidators(PropertyInfo propertyInfo)
        {
            Validators = ResolveValidators(propertyInfo.CustomAttributes)
                .Union(ResolveValidators(propertyInfo.PropertyType.CustomAttributes))
                .ToArray();

            return this;
        }

        internal BaseStrategy EnhanceWithValidators(Type type)
        {
            Validators = ResolveValidators(type.CustomAttributes);

            return this;
        }

        private Validator[] ResolveValidators(IEnumerable<CustomAttributeData> attributes)
        {
            var validatorsDictionary = new Dictionary<Type, Func<CustomAttributeData, Validator>>
            {
                { typeof(RequiredAttribute), _ => new Validator(ValidatorType.Required) },
                {
                    typeof(MinLengthAttribute),
                    attributeData => new Validator(ValidatorType.MinLength)
                        { Value = attributeData.ConstructorArguments.First().Value }
                },
                {
                    typeof(MaxLengthAttribute),
                    attributeData => new Validator(ValidatorType.MaxLength)
                        { Value = attributeData.ConstructorArguments.First().Value }
                }
            };

            return attributes
                .Select(x => validatorsDictionary.TryGetValue(x.AttributeType, out var func) ? func(x) : null)
                .Where(x => x != null)
                .ToArray();
        }
    }
}