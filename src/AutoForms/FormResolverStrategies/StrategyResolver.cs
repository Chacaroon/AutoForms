namespace AutoForms.FormResolverStrategies
{
    using AutoForms.FormResolverStrategies.Strategies;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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
            var strategy = _strategies.Value.First(x => x.IsStrategyApplicable(modelType, strategyOptions));

            return strategy;
        }

        public BaseStrategy Resolve(PropertyInfo propertyInfo)
        {
            var strategyOptions = _strategyOptionsResolver.GetStrategyOptions(propertyInfo);
            var strategy = _strategies.Value.First(x => x.IsStrategyApplicable(propertyInfo.PropertyType, strategyOptions));

            return strategy;
        }
    }
}
