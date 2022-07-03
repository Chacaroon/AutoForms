using System.Collections.Generic;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Processors;
using AutoForms.Resolvers;

namespace AutoForms.Strategies;

internal class FormControlStrategy : BaseStrategy
{
    public FormControlStrategy(IEnumerable<BaseControlProcessor> controlProcessors)
        : base(controlProcessors)
    {
    }

    internal override bool IsStrategyApplicable(FormBuilderContext context)
    {
        return PropertyFormControlTypeResolver.IsFormControl(context);
    }

    internal override AbstractControl Process(FormBuilderContext context, HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, context.ModelType);

        var control = new FormControl { Value = context.Value };

        ProcessControl(control, context);

        return control;
    }
}
