using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormBuilder.UnitTests")]
namespace FormBuilder.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormControlStrategy : BaseStrategy
    {
        internal override bool IsStrategyApplicable(Type modelType) => 
            PropertyFormControlTypeResolver.IsPrimitive(modelType);

        internal override Node Process(string name, Type type)
        {
            return new FormControl()
            {
                Name = name
            };
        }
    }
}
