using System.Collections.Generic;
using System.Reflection;
using AutoForms.Extensions;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Processors;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormGroupStrategy : BaseStrategy
{
    private readonly StrategyResolver _strategyResolver;

    public FormGroupStrategy(StrategyResolver strategyResolver,
        IEnumerable<BaseControlProcessor> controlProcessors)
        : base(controlProcessors)
    {
        _strategyResolver = strategyResolver;
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormGroup(context);
    }

    internal override AbstractControl Process(FormBuilderContext context, HashSet<Type> hashSet)
    {
        AbstractControl BuildControl(PropertyInfo propertyInfo, object? value)
        {
            var itemContext = context with
            {
                ModelType = propertyInfo.PropertyType,
                PropertyInfo = propertyInfo,
                Value = value,
            };
            return _strategyResolver.Resolve(itemContext)
                .ProcessInternal(itemContext, hashSet);
        }

        var properties = context.ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var controls = properties.ToDictionary(x => x.Name.FirstCharToLowerCase(),
            x => BuildControl(x, GetPropertyValue(x, context.Value)));

        var control = new FormGroup
        {
            Controls = controls
        };
        
        return control;
    }

    private object? GetPropertyValue(PropertyInfo propertyInfo, object? value)
    {
        return value == null ? null : propertyInfo.GetValue(value);
    }
}
