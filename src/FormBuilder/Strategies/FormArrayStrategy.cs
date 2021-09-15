namespace FormBuilder.Strategies
{
    using System;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormArrayStrategy : BaseStrategy
    {
        internal override bool IsStrategyApplicable(Type modelType)
        {
            return !PropertyFormControlTypeResolver.IsPrimitive(modelType)
                   && PropertyFormControlTypeResolver.IsCollection(modelType);
        }

        internal override Node Process(string name, Type type)
        {
            var formArray = new FormArray
            {
                Name = name
            };

            return formArray;
        }
    }
}
