namespace AutoForms.FormResolverStrategies.Strategies
{
    using AutoForms.Helpers;
    using AutoForms.Models;
    using AutoForms.Options;
    using System;

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
