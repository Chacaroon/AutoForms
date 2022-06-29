using System.Collections.Generic;
using System.Reflection;
using AutoForms.Extensions;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

internal class FormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(Type modelType, ResolvingStrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsFormGroup(modelType, options);
    }

    internal override AbstractControl Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        AbstractControl BuildControl(PropertyInfo propertyInfo, object value)
        {
            return _strategyResolver.Resolve(propertyInfo, Options)
                .EnhanceWithValue(value)
                .Process(propertyInfo.PropertyType, hashSet);
        }

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var controls = properties.ToDictionary(x => x.Name.FirstCharToLowerCase(),
            x => BuildControl(x, GetPropertyValue(x, Value)));

        return new FormGroup
        {
            Controls = controls
        };
    }

    private object GetPropertyValue(PropertyInfo propertyInfo, object value)
    {
        if (value == null)
        {
            return null;
        }

        return propertyInfo.GetValue(value);
    }
}
