using System.Collections.Generic;
using AutoForms.Enums;

namespace AutoForms.Models;


/// <summary>
/// Represents control that holds collection of values.
/// </summary>
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
