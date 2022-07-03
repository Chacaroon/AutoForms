using System.Collections.Generic;
using System.Reflection;
using AutoForms.Options;
using AutoForms.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace AutoForms.Resolvers;

internal class StrategyResolver
{
    private readonly Lazy<IEnumerable<BaseStrategy>> _strategies;

    internal StrategyResolver(IServiceProvider serviceProvider)
    {
        _strategies = new Lazy<IEnumerable<BaseStrategy>>(serviceProvider.GetServices<BaseStrategy>);
    }

    internal BaseStrategy Resolve(FormBuilderContext context)
    {
        var strategy = _strategies.Value.First(x => x.IsStrategyApplicable(context));

        strategy.Context = context;

        return strategy;
    }
}
