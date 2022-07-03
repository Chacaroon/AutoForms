using System.Collections.Generic;
using AutoForms.Exceptions;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.Strategies;

internal abstract class BaseStrategy
{
    private protected Validator[] Validators { get; } = Array.Empty<Validator>();

    internal FormBuilderContext Context { get; set; } = null!;

    #region AbstractMethods

    internal abstract bool IsStrategyApplicable(FormBuilderContext context);

    internal abstract AbstractControl Process(HashSet<Type> hashSet);

    #endregion

    #region Helpers

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
