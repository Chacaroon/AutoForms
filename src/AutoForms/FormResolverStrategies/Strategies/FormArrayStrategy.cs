namespace AutoForms.FormResolverStrategies.Strategies
{
    using AutoForms.Helpers;
    using AutoForms.Models;
    using AutoForms.Options;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class FormArrayStrategy : BaseStrategy
    {
        private readonly StrategyResolver _strategyResolver;

        public FormArrayStrategy(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
        {
            return !PropertyFormControlTypeResolver.IsFormControl(modelType, options)
                   && PropertyFormControlTypeResolver.IsFormArray(modelType, options);
        }

        internal override Node Process(Type type)
        {
            var collectionItemType = GetCollectionItemType(type);
            var values = ((IEnumerable)Value)?.Cast<object>() ?? Array.Empty<object>();

            BaseStrategy GetStrategy(object value)
            {
                return _strategyResolver.Resolve(collectionItemType)
                    .EnhanceWithValue(value);
            }

            var nodes = values.Select(x => GetStrategy(x).Process(collectionItemType));

            var formArray = new FormArray
            {
                Nodes = nodes
            };

            return formArray;
        }

        private Type GetCollectionItemType(Type collectionType)
        {
            var interfaces = collectionType.GetInterfaces().Union(new[] { collectionType });

            var enumerableInterfaceType = interfaces.First(x =>
                x.IsGenericType
                && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            var collectionItemType = enumerableInterfaceType
                .GetGenericArguments()
                .Single();

            return collectionItemType;
        }
    }
}
