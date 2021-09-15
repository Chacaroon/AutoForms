namespace FormBuilder.Strategies
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormArrayStrategy : BaseStrategy
    {
        private readonly StrategyResolver _strategyResolver;

        public FormArrayStrategy(StrategyResolver strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        internal override bool IsStrategyApplicable(Type modelType)
        {
            return !PropertyFormControlTypeResolver.IsPrimitive(modelType)
                   && PropertyFormControlTypeResolver.IsCollection(modelType);
        }

        internal override Node Process(string name, Type type)
        {
            var collectionItemType = GetCollectionItemType(type);
            var values = ((IEnumerable)Value)?.Cast<object>() ?? Array.Empty<object>();

            BaseStrategy GetStrategy(object value)
            {
                return _strategyResolver.Resolve(collectionItemType)
                    .EnhanceWithValue(value);
            }

            var nodes = values.Select(x => GetStrategy(x).Process(null, collectionItemType));

            var formArray = new FormArray
            {
                Name = name,
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
