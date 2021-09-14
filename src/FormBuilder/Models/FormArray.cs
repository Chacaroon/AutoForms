namespace FormBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FormBuilder.Enums;

    internal class FormArray : Node
    {
        public override NodeType Type => NodeType.Array;

        public IEnumerable<Node> Nodes { get; set; }
    }
}
