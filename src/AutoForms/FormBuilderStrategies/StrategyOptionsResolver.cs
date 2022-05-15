namespace AutoForms.FormBuilderStrategies;

using System.Reflection;
using AutoForms.Attributes;
using AutoForms.Options;

public class StrategyOptionsResolver
{
    public StrategyOptions GetStrategyOptions(PropertyInfo propertyInfo)
    {
        return new StrategyOptions
        {
            IsFormValue = HasAttribute<FormValueAttribute>(propertyInfo)
                          || HasAttribute<FormValueAttribute>(propertyInfo.PropertyType)
        };
    }

    public StrategyOptions GetStrategyOptions(Type type)
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
