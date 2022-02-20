namespace AutoForms.Models
{
    using AutoForms.Enums;

    internal class FormControl : Node
    {
        public override NodeType Type => NodeType.Control;

        public object Value { get; set; }
    }
}
