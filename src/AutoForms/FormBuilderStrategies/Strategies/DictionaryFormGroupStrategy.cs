using System.Collections;
using System.Collections.Generic;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

internal class DictionaryFormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public DictionaryFormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(Type modelType, ResolvingStrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsDictionary(modelType, options);
    }

    internal override AbstractControl Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        if (Value == null)
        {
            return new FormGroup();
        }

        var valueType = GetDictionaryValueType(type);

        AbstractControl BuildControl(object value) =>
            _strategyResolver.Resolve(valueType, Options)
                .EnhanceWithValue(value)
                .Process(valueType, hashSet);

        var value = (Value as IDictionary)!;

        var result = value.Keys.Cast<object>().ToDictionary(key => key.ToString(),
            key => BuildControl(value[key]));

        return new FormGroup
        {
            Controls = result
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
