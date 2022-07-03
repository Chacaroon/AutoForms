using System.Collections;
using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Processors;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormArrayStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormArrayStrategy(StrategyResolver strategyResolver,
        IEnumerable<BaseControlProcessor> controlProcessors)
        : base(controlProcessors)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormArray(context);
    }

    internal override AbstractControl Process(FormBuilderContext context, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, context.ModelType);

        var collectionItemType = GetCollectionItemType(context.ModelType);

        AbstractControl BuildControl(object? value = null)
        {
            var collectionItemContext = context with
            {
                ModelType = collectionItemType,
                PropertyInfo = null,
                Value = value
            };
            return _strategyResolver.Resolve(collectionItemContext)
                .Process(collectionItemContext, hashSet);
        }

        var values = ((IEnumerable?)context.Value)?.Cast<object>() ?? Array.Empty<object>();
        var controls = values.Select(BuildControl);

        var formArray = new FormArray(controls, BuildControl());

        ProcessControl(formArray, context);

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
