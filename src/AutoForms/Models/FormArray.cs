namespace AutoForms.Models
{
    using AutoForms.Enums;
    using System;
    using System.Collections.Generic;

    internal class FormArray : Node
    {
        public FormArray()
        {
            Nodes = Array.Empty<Node>();
        }

        public override NodeType Type => NodeType.Array;

        public IEnumerable<Node> Nodes { get; set; }
    }
}
