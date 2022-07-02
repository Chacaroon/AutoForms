using System.Collections;
using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class DictionaryFormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public DictionaryFormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext options)
    {
        return PropertyFormControlTypeResolver.IsDictionary(options);
    }

    internal override AbstractControl Process(HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, Context.ModelType);

        if (Context.Value == null)
        {
            return new FormGroup();
        }

        var valueType = GetDictionaryValueType(Context.ModelType);

        AbstractControl BuildControl(object? value) =>
            _strategyResolver.Resolve(Context with
                {
                    ModelType = valueType,
                    PropertyInfo = null,
                    Value = value
                })
                .Process(hashSet);

        var value = (IDictionary)Context.Value!;

        var result = value.Keys.Cast<object>()
            .ToDictionary(key => key.ToString()!, key => BuildControl(value[key]));

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
