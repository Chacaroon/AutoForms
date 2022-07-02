using AutoForms.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AutoForms.Models;

/// <summary>
/// Represents control that holds indivisible value.
/// </summary>
[ExcludeFromCodeCoverage]
public class FormControl : AbstractControl
{
    /// <inheritdoc />
    public override ControlType Type => ControlType.Control;

    /// <summary>
    /// The FormControl's value
    /// </summary>
    public object? Value { get; set; }
}
