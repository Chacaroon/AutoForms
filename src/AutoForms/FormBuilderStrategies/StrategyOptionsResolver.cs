using System.Reflection;
using AutoForms.Attributes;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies;

internal class StrategyOptionsResolver
{
    internal StrategyOptions GetStrategyOptions(PropertyInfo propertyInfo)
    {
        return new StrategyOptions
        {
            IsFormValue = HasAttribute<FormValueAttribute>(propertyInfo)
                          || HasAttribute<FormValueAttribute>(propertyInfo.PropertyType)
        };
    }

    internal StrategyOptions GetStrategyOptions(Type type)
    {
        return new StrategyOptions
        {
            IsFormValue = HasAttribute<FormValueAttribute>(type)
        };
    }

    private bool HasAttribute<TAttribute>(PropertyInfo propertyInfo)
        where TAttribute : Attribute
    {
        return propertyInfo.GetCustomAttribute<TAttribute>(true) != null;
    }

    private bool HasAttribute<TAttribute>(Type propertyInfo)
        where TAttribute : Attribute
    {
        return propertyInfo.GetCustomAttribute<TAttribute>(true) != null;
    }
}
