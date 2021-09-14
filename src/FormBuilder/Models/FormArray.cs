namespace FormBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using FormBuilder.Enums;

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
