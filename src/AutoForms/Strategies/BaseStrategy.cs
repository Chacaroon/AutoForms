using System.Collections.Generic;
using AutoForms.Exceptions;
using AutoForms.Models;
using AutoForms.Options;
using AutoForms.Processors;

namespace AutoForms.Strategies;

internal abstract class BaseStrategy
{
    private readonly IEnumerable<BaseControlProcessor> _controlProcessors;

    internal BaseStrategy(IEnumerable<BaseControlProcessor> controlProcessors)
    {
        _controlProcessors = controlProcessors;
    }

    #region AbstractMethods

    internal abstract bool IsStrategyApplicable(FormBuilderContext context);

    internal abstract AbstractControl Process(FormBuilderContext context, HashSet<Type> hashSet);

    #endregion

    #region Helpers

    internal AbstractControl ProcessInternal(FormBuilderContext context, HashSet<Type> hashSet)
    {
        PreProcess(context, ref hashSet);

        var control = Process(context, hashSet);

        PostProcess(control, context);

        return control;
    }

    private void PreProcess(FormBuilderContext context, ref HashSet<Type> hashSet)
    {
        CheckCircularDependency(ref hashSet, context.ModelType);
    }

    private void PostProcess(AbstractControl control, FormBuilderContext context)
    {
        ProcessControl(control, context);
    }

    private void ProcessControl(AbstractControl control, FormBuilderContext context)
    {
        foreach (var controlProcessor in _controlProcessors)
        {
            controlProcessor.Process(control, context);
        }
    }

    private void CheckCircularDependency(ref HashSet<Type> hashSet, Type type)
    {
        if (hashSet.Contains(type))
        {
            var typesArray = hashSet.Concat(new[] { type }).ToArray();
            typesArray = typesArray.Skip(Array.IndexOf(typesArray, type)).ToArray();
            var path = string.Join("->", typesArray.Select(x => x.Name));
            throw new CircularDependencyException($"Circular dependency: {path}");
        }
        
        hashSet = new(hashSet.Union(new[] { type }));
    }

    #endregion
}
