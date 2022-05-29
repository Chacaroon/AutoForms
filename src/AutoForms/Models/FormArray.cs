using System.Collections.Generic;
using AutoForms.Enums;

namespace AutoForms.Models;

public class FormArray : Node
{
    public FormArray()
    {
        Nodes = Array.Empty<Node>();
    }

    public override NodeType Type => NodeType.Array;

    public IEnumerable<Node> Nodes { get; set; }

    public Node NodeSchema { get; set; }
}
