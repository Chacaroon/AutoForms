using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoForms.Enums;

namespace AutoForms.Models;


/// <summary>
/// Represents control that holds collection of values.
/// </summary>
[ExcludeFromCodeCoverage]
public class FormArray : AbstractControlCollectionBase<IEnumerable<AbstractControl>>
{
    /// <summary>
    /// Creates the <see cref="FormArray"/> instance
    /// </summary>
    public FormArray(IEnumerable<AbstractControl> controls, AbstractControl controlSchema)
    {
        Controls = controls;
        ControlSchema = controlSchema;
    }

    /// <inheritdoc/>
    public override ControlType Type => ControlType.Array;

    /// <summary>
    /// The structure of the child control.
    /// </summary>
    /// <remarks>
    /// Uses by another system's part to build and add new controls to the FormArray.
    /// </remarks>
    public AbstractControl ControlSchema { get; set; }
}
