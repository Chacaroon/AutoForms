using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormControlStrategy : BaseStrategy
{
    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormControl(context);
    }

    internal override AbstractControl Process(HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, Context.ModelType);

        return new FormControl
        {
            Value = Context.Value,
            Validators = Validators
        };
    }
}
