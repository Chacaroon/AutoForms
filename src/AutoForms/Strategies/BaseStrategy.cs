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

    protected void ProcessControl(AbstractControl control, FormBuilderContext context)
    {
        foreach (var controlProcessor in _controlProcessors)
        {
            controlProcessor.Process(control, context);
        }
    }

    protected void CheckCircularDependency(ref HashSet<Type> hashSet, Type type)
    {
        if (hashSet.Contains(type))
        {
            var typesArray = hashSet.Concat(new[] { type }).ToArray();
            typesArray = typesArray[Array.IndexOf(typesArray, type)..];
            var path = string.Join("->", typesArray.Select(x => x.Name));
            throw new CircularDependencyException($"Circular dependency: {path}");
        }

        hashSet = hashSet.Union(new[] { type }).ToHashSet(hashSet.Comparer);
    }

    #endregion
}
