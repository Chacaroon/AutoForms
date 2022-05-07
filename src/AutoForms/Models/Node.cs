namespace AutoForms.Models
{
    using AutoForms.Enums;

    public abstract class Node
    {
        public abstract NodeType Type { get; }

        public Validator[] Validators { get; set; }
    }
}
