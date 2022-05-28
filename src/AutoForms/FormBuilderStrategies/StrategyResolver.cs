using System.Collections.Generic;
using System.Reflection;
using AutoForms.FormBuilderStrategies.Strategies;
using AutoForms.Options;
using Microsoft.Extensions.DependencyInjection;

namespace AutoForms.FormBuilderStrategies;

public class StrategyResolver
{
    private readonly StrategyOptionsResolver _strategyOptionsResolver;
    private readonly Lazy<IEnumerable<BaseStrategy>> _strategies;

    public StrategyResolver(IServiceProvider serviceProvider,
        StrategyOptionsResolver strategyOptionsResolver)
    {
        _strategyOptionsResolver = strategyOptionsResolver;
        _strategies = new Lazy<IEnumerable<BaseStrategy>>(serviceProvider.GetServices<BaseStrategy>);
    }

    public BaseStrategy Resolve(Type modelType)
    {
        var strategyOptions = _strategyOptionsResolver.GetStrategyOptions(modelType);

        return Resolve(modelType, strategyOptions);
    }

    public BaseStrategy Resolve(PropertyInfo propertyInfo)
    {
        var strategyOptions = _strategyOptionsResolver.GetStrategyOptions(propertyInfo);

        return Resolve(propertyInfo.PropertyType, strategyOptions);
    }

    private BaseStrategy Resolve(Type modelType, StrategyOptions strategyOptions)
    {
        var strategy = _strategies.Value.First(x => x.IsStrategyApplicable(modelType, strategyOptions));

        return strategy;
    }
}
