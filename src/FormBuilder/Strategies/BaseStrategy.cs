namespace FormBuilder.Strategies
{
    using System;
    using FormBuilder.Models;

    internal abstract class BaseStrategy
    {
        internal abstract bool IsStrategyApplicable(Type modelType);

        internal abstract Node Process(string name, Type type);
    }
}
