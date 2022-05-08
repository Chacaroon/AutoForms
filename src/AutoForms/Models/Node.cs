namespace AutoForms.Models
{
    using System.Runtime.Serialization;
    using AutoForms.Enums;

    [KnownType(typeof(FormControl))]
    [KnownType(typeof(FormGroup))]
    [KnownType(typeof(FormArray))]
    public abstract class Node
    {
        public abstract NodeType Type { get; }

        public Validator[] Validators { get; set; }
    }
}
