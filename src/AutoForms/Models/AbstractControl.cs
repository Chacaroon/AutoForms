using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using AutoForms.Enums;

namespace AutoForms.Models;

/// <summary>
/// Base class from all types of controls.
/// </summary>
[KnownType(typeof(FormControl))]
[KnownType(typeof(FormGroup))]
[KnownType(typeof(FormArray))]
[ExcludeFromCodeCoverage]
public abstract class AbstractControl
{
    /// <summary>
    /// The control type
    /// </summary>
    public abstract ControlType Type { get; }

    /// <summary>
    /// Validators' metadata
    /// </summary>
    public Validator[] Validators { get; set; } = Array.Empty<Validator>();
}
