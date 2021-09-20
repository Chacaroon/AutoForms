namespace FormBuilder.FormResolverStrategies.Strategies
{
    using System;
    using FormBuilder.Models;
using FormBuilder.Options;

    internal abstract class BaseStrategy
    {
        private protected object Value { get; private set; }

        internal abstract bool IsStrategyApplicable(Type modelType, StrategyOptions options);

        internal abstract Node Process(Type type);

        internal BaseStrategy EnhanceWithValue(object value)
        {
            Value = value;

            return this;
        }
    }
}
