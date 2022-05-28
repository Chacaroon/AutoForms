using AutoForms.Enums;

namespace AutoForms.Models;

internal class FormControl : Node
{
    public override NodeType Type => NodeType.Control;

    public object Value { get; set; }
}
