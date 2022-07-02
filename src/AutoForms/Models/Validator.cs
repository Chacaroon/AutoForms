using AutoForms.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AutoForms.Models;

/// <summary>
/// The validator metadata
/// </summary>
[ExcludeFromCodeCoverage]
public class Validator
{
    /// <summary>
    /// Creates the <see cref="Validator"/> instance
    /// </summary>
    /// <param name="type">The validator type</param>
    public Validator(ValidatorType type)
    {
        Type = type;
    }

    /// <summary>
    /// The validator type
    /// </summary>
    public ValidatorType Type { get; }

    /// <summary>
    /// The validation message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Validator additional options
    /// </summary>
    public virtual object? Value { get; set; }
}
