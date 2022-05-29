using System.Runtime.Serialization;
using AutoForms.Enums;

namespace AutoForms.Models;

/// <summary>
/// Base class from all types of controls.
/// </summary>
[KnownType(typeof(FormControl))]
[KnownType(typeof(FormGroup))]
[KnownType(typeof(FormArray))]
public abstract class Node
{
    public abstract NodeType Type { get; }

    public Validator[] Validators { get; set; }
}
