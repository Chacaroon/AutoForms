using AutoForms.Models;
using AutoForms.Strategies;

namespace AutoForms;


/// <summary>
/// The class that build a schema of the passed type.
/// </summary>
public class FormBuilder
{
    private readonly Type _type;
    private readonly BaseStrategy _strategy;
    private object? _value;

    internal FormBuilder(Type type, BaseStrategy strategy)
    {
        _type = type;
        _strategy = strategy;
    }

    /// <summary>
    /// Populate controls with the value of the <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value with which the controls will be populated.</param>
    /// <returns>The same instance of the <see cref="FormBuilder"/> for chaining.</returns>
    public FormBuilder EnhanceWithValue(object? value)
    {
        if (value != null && !_type.IsInstanceOfType(value))
            throw new ArgumentException("The value type does not match the type the FormBuilder was created for.", nameof(value));

        _value = value;

        return this;
    }

    /// <summary>
    /// Build data structure.
    /// </summary>
    /// <returns>The root control of the built data structure.
    /// </returns>
    /// <remarks>
    /// Each returned control is an instance of a <seealso cref="FormControl"/>,
    /// <seealso cref="FormGroup"/> or <seealso cref="FormArray"/>.
    /// </remarks>
    public AbstractControl Build()
    {
        return _strategy.ProcessInternal(new(_type) { Value = _value }, new());
    }
}
