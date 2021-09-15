namespace FormBuilder.Models
{
    using FormBuilder.Enums;

    internal abstract class Node
    {
        public abstract NodeType Type { get; }

        public string Name { get; set; }

        public object Value { get; set; }
    }
}
