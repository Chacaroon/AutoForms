using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoForms.Enums;

namespace AutoForms.Models;


/// <summary>
/// Represents control that holds object-like value.
/// </summary>
[ExcludeFromCodeCoverage]
public class FormGroup : AbstractControl
{
    /// <inheritdoc />
    public override ControlType Type => ControlType.Group;

    /// <summary>
    /// Dictionary with the child controls.
    /// </summary>
    public Dictionary<string, AbstractControl> Controls { get; set; } = new();
}
