using System.Collections.Generic;
using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

internal class FormControlStrategy : BaseStrategy
{
    internal override bool IsStrategyApplicable(Type modelType, ResolvingStrategyOptions options)
    {
        return PropertyFormControlTypeResolver.IsFormControl(modelType, options);
    }

    internal override Node Process(Type type, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, type);

        return new FormControl
        {
            Value = Value,
            Validators = Validators
        };
    }
}
