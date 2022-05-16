using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;
using System.Collections;
using System.Collections.Generic;

namespace AutoForms.FormBuilderStrategies.Strategies;

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
               && !PropertyFormControlTypeResolver.IsDictionary(modelType)
               && PropertyFormControlTypeResolver.IsFormArray(modelType, options);
    }

    internal override Node Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        var collectionItemType = GetCollectionItemType(type);
        var values = ((IEnumerable)Value)?.Cast<object>() ?? Array.Empty<object>();

        Node BuildNode(object value) => _strategyResolver.Resolve(collectionItemType)
            .EnhanceWithValidators(collectionItemType)
            .EnhanceWithValue(value)
            .Process(collectionItemType, hashSet);

        var nodes = values.Select(BuildNode);

        var formArray = new FormArray
        {
            Nodes = nodes,
            NodeSchema = BuildNode(null)
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
