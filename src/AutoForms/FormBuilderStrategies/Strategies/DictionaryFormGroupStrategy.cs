using System.Collections.Generic;

namespace AutoForms.FormBuilderStrategies.Strategies;

using System.Collections;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;

internal class DictionaryFormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public DictionaryFormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsDictionary(modelType, options);
    }

    internal override Node Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        if (Value == null)
        {
            return new FormGroup();
        }

        var valueType = GetDictionaryValueType(type);

        Node BuildNode(object value) =>
            _strategyResolver.Resolve(valueType)
                .EnhanceWithValue(value)
                .EnhanceWithValidators(valueType)
                .Process(value.GetType(), hashSet);

        var value = (Value as IDictionary)!;

        var result = value.Keys.Cast<object>().ToDictionary(key => key.ToString(),
            key => BuildNode(value[key]));

        return new FormGroup
        {
            Nodes = result
        };
    }

    private Type GetDictionaryValueType(Type type)
    {
        var genericArguments = type.GetInterfaces()
            .Concat(new[] { type })
            .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            .GetGenericArguments();

        return genericArguments[1];
    }
}
