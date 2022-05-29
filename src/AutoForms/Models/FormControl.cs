using AutoForms.Enums;

namespace AutoForms.Models;

public class FormControl : Node
{
    public override NodeType Type => NodeType.Control;

    public object Value { get; set; }
}
