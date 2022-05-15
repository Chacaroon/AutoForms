namespace AutoForms.Models;

using AutoForms.Enums;
using System.Runtime.Serialization;

[KnownType(typeof(FormControl))]
[KnownType(typeof(FormGroup))]
[KnownType(typeof(FormArray))]
public abstract class Node
{
    public abstract NodeType Type { get; }

    public Validator[] Validators { get; set; }
}
