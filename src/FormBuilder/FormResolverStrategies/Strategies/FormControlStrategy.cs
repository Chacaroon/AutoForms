namespace FormBuilder.FormResolverStrategies.Strategies
{
    using System;
    using FormBuilder.Helpers;
    using FormBuilder.Models;
using FormBuilder.Options;

    internal class FormControlStrategy : BaseStrategy
    {
        internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
        {
            return PropertyFormControlTypeResolver.IsFormControl(modelType, options);
        }

        internal override Node Process(Type type)
        {
            return new FormControl
            {
                Value = Value
            };
        }
    }
}
