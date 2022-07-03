using System.Collections.Generic;
using System.Reflection;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormGroup(context);
    }

    internal override AbstractControl Process(HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, Context.ModelType);

        AbstractControl BuildControl(PropertyInfo propertyInfo, object? value)
        {
            return _strategyResolver.Resolve(Context with
                {
                    ModelType = propertyInfo.PropertyType,
                    PropertyInfo = propertyInfo,
                    Value = value,
                })
                .Process(hashSet);
        }

        var properties = Context.ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var controls = properties.ToDictionary(x => x.Name.FirstCharToLowerCase(),
            x => BuildControl(x, GetPropertyValue(x, Context.Value)));

        return new FormGroup
        {
            Controls = controls
        };
    }

    private object? GetPropertyValue(PropertyInfo propertyInfo, object? value)
    {
        if (value == null)
        {
            return null;
        }

        return propertyInfo.GetValue(value);
    }
}
