using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormBuilder.UnitTests")]
namespace FormBuilder.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    internal class StrategyResolver
    {
        private readonly Lazy<IEnumerable<BaseStrategy>> _strategies;

        public StrategyResolver(IServiceProvider serviceProvider)
        {
            _strategies = new Lazy<IEnumerable<BaseStrategy>>(serviceProvider.GetServices<BaseStrategy>);
        }

        public BaseStrategy Resolve(Type modelType)
        {
            var strategy = _strategies.Value.First(x => x.IsStrategyApplicable(modelType));

            return strategy;
        }
    }
}
