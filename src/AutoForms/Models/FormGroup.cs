using System.Collections.Generic;
using AutoForms.Enums;

namespace AutoForms.Models;


/// <summary>
/// Represents control that holds object-like value.
/// </summary>
public class FormGroup : Node
{
    public override NodeType Type => NodeType.Group;

    public Dictionary<string, Node> Nodes { get; set; } = new();
}
