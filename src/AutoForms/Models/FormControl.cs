using AutoForms.Enums;

namespace AutoForms.Models;

/// <summary>
/// Represents control that holds indivisible value.
/// </summary>
public class FormControl : Node
{
    public override NodeType Type => NodeType.Control;

    public object Value { get; set; }
}
