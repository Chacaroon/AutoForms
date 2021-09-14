using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FormBuilder.UnitTests")]
namespace FormBuilder.Strategies
{
    using System;
    using System.Collections;
    using FormBuilder.Helpers;
    using FormBuilder.Models;

    internal class FormArrayStrategy : BaseStrategy
    {
        internal override bool IsStrategyApplicable(Type modelType) => 
            !PropertyFormControlTypeResolver.IsPrimitive(modelType)
            && PropertyFormControlTypeResolver.IsCollection(modelType);
        
        internal override Node Process(string name, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
