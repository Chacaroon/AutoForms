namespace FormBuilder.Models
{
    using FormBuilder.Enums;

    internal class FormControl : Node
    {
        public override NodeType Type => NodeType.Control;

        public object Value { get; set; }
    }
}
