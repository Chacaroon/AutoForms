using AutoForms.Models;
using AutoForms.Options;
using System.Diagnostics;

namespace AutoForms.Processors;

/// <summary>
/// Base class for the control processors. 
/// </summary>
public abstract class BaseControlProcessor
{
    /// <summary>
    /// Process the control
    /// </summary>
    /// <param name="control">The processed control</param>
    /// <param name="context">The control's context</param>
    public abstract void Process(AbstractControl control, FormBuilderContext context);
}
