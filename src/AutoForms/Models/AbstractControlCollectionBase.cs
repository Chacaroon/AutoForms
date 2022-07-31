using System.Diagnostics.CodeAnalysis;

namespace AutoForms.Models;

/// <summary>
/// Base type for controls that holds another ones.
/// </summary>
/// <typeparam name="T">The controls collection type</typeparam>
[ExcludeFromCodeCoverage]
public abstract class AbstractControlCollectionBase<T> : AbstractControl
{
    /// <summary>
    /// The child controls.
    /// </summary>
    public T Controls { get; set; } = default!;
}
