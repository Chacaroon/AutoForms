namespace FormBuilder.Strategies
{
    using System;
    using FormBuilder.Models;

    internal abstract class BaseStrategy
    {
        private protected object Value { get; private set; }

        internal abstract bool IsStrategyApplicable(Type modelType);

        internal abstract Node Process(Type type);

        internal BaseStrategy EnhanceWithValue(object value)
        {
            Value = value;

            return this;
        }
    }
}
