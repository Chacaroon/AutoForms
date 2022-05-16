namespace AutoForms.FormBuilderStrategies.Strategies;

using AutoForms.Extensions;
using AutoForms.FormBuilderStrategies;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;
using System.Reflection;

internal class FormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormGroupStrategy(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsFormGroup(modelType, options);
    }

    internal override Node Process(Type type)
    {
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var nodes = properties.ToDictionary(x => x.Name.FirstCharToLowerCase(), x =>
            BuildNode(x, GetPropertyValue(x, Value)));

        return new FormGroup
        {
            Nodes = nodes
        };
    }

    private Node BuildNode(PropertyInfo propertyInfo, object value)
    {
        return _strategyResolver.Resolve(propertyInfo)
            .EnhanceWithValue(value)
            .EnhanceWithValidators(propertyInfo)
            .Process(propertyInfo.PropertyType);
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
