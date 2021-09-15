namespace FormBuilder.Strategies
{
    using System;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormControlStrategy : BaseStrategy
    {
        internal override bool IsStrategyApplicable(Type modelType)
        {
            return PropertyFormControlTypeResolver.IsPrimitive(modelType);
        }

        internal override Node Process(string name, Type type)
        {
            return new FormControl
            {
                Name = name,
                Value = Value
            };
        }
    }
}
