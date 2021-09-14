namespace FormBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FormBuilder.Enums;

    internal class FormControl : Node
    {
        public override NodeType Type => NodeType.Control;

        public object Value { get; set; }
    }
}
