using System.Collections;
using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormArrayStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormArrayStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormArray(context);
    }

    internal override AbstractControl Process(HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, Context.ModelType);

        var collectionItemType = GetCollectionItemType(Context.ModelType);

        AbstractControl BuildControl(object? value = null)
        {
            var collectionItemContext = Context with
            {
                ModelType = collectionItemType,
                PropertyInfo = null,
                Value = value
            };
            return _strategyResolver.Resolve(collectionItemContext)
                .Process(hashSet);
        }

        var values = ((IEnumerable?)Context.Value)?.Cast<object>() ?? Array.Empty<object>();
        var controls = values.Select(BuildControl);

        var formArray = new FormArray(controls, BuildControl());

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
