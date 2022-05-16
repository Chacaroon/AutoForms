namespace AutoForms.Models;

using AutoForms.Enums;
using System.Collections.Generic;

internal class FormGroup : Node
{
    public override NodeType Type => NodeType.Group;

    public Dictionary<string, Node> Nodes { get; set; } = new();
}
