using System.Collections;
using System.Collections.Generic;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

internal class FormArrayStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormArrayStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(Type modelType, ResolvingStrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsFormArray(modelType, options);
    }

    internal override Node Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        var collectionItemType = GetCollectionItemType(type);

        Node BuildNode(object value = null) => _strategyResolver.Resolve(collectionItemType, Options)
            .EnhanceWithValue(value)
            .Process(collectionItemType, hashSet);

        var values = ((IEnumerable)Value)?.Cast<object>() ?? Array.Empty<object>();
        var nodes = values.Select(BuildNode);

        var formArray = new FormArray
        {
            Nodes = nodes,
            NodeSchema = BuildNode()
        };

        return formArray;
    }

    private Type GetCollectionItemType(Type collectionType)
    {
        var interfaces = collectionType.GetInterfaces().Concat(new[] { collectionType });

        var enumerableInterfaceType = interfaces.FirstOrDefault(x =>
            x.IsGenericType
            && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        var collectionItemType = enumerableInterfaceType?
            .GetGenericArguments()
            .Single();

        return collectionItemType ?? typeof(object);
    }
}
