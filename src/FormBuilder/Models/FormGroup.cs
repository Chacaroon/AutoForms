namespace FormBuilder.Models
{
    using System.Collections.Generic;
    using FormBuilder.Enums;

    internal class FormGroup : Node
    {
        public override NodeType Type => NodeType.Group;

        public IEnumerable<Node> Nodes { get; set; }
    }
}
