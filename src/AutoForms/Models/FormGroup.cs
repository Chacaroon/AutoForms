using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoForms.Enums;

namespace AutoForms.Models;


/// <summary>
/// Represents control that holds object-like value.
/// </summary>
[ExcludeFromCodeCoverage]
public class FormGroup : AbstractControlCollectionBase<Dictionary<string, AbstractControl>>
{
    /// <inheritdoc />
    public override ControlType Type => ControlType.Group;
}
