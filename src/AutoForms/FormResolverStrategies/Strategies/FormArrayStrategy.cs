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
        private readonly FormResolver _formResolver;

        public FormArrayStrategy(FormResolver formResolver)
        {
            _formResolver = formResolver;
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

            var nodes = values.Select(value => _formResolver.Resolve(collectionItemType, value));

            var formArray = new FormArray
            {
                Nodes = nodes,
                NodeSchema = _formResolver.Resolve(collectionItemType)
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
