using System.Diagnostics.CodeAnalysis;

namespace AutoForms.Models;

/// <summary>
/// Base type for abstract control that holds another controls.
/// </summary>
/// <typeparam name="T">The controls collection type</typeparam>
[ExcludeFromCodeCoverage]
public abstract class AbstractControlCollectionBase<T> : AbstractControl
{
    /// <summary>
    /// Collection of the child controls.
    /// </summary>
    public T Controls { get; set; } = default!;
}
