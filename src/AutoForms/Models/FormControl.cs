using AutoForms.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AutoForms.Models;

/// <summary>
/// Represents control that holds indivisible value.
/// </summary>
[ExcludeFromCodeCoverage]
public class FormControl : Node
{
    public override NodeType Type => NodeType.Control;

    public object Value { get; set; }
}
