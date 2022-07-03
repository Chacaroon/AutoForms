using System.Collections;
using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Processors;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class DictionaryFormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public DictionaryFormGroupStrategy(StrategyResolver strategyResolver,
        IEnumerable<BaseControlProcessor> controlProcessors)
        : base(controlProcessors)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsDictionary(context);
    }

    internal override AbstractControl Process(FormBuilderContext context, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, context.ModelType);

        if (context.Value == null)
        {
            return new FormGroup();
        }

        var valueType = GetDictionaryValueType(context.ModelType);

        AbstractControl BuildControl(object? value)
        {
            var itemContext = context with
            {
                ModelType = valueType,
                PropertyInfo = null,
                Value = value
            };
            return _strategyResolver
                .Resolve(itemContext)
                .Process(itemContext, hashSet);
        }

        var value = (IDictionary)context.Value!;

        var result = value.Keys.Cast<object>()
            .ToDictionary(key => key.ToString()!, key => BuildControl(value[key]));

        var control = new FormGroup
        {
            Controls = result
        };

        ProcessControl(control, context);

        return control;
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
