using AutoForms.FormBuilderStrategies.Strategies;
using AutoForms.Models;

namespace AutoForms;


/// <summary>
/// The class that build a schema of the passed type.
/// </summary>
public class FormBuilder
{
    private readonly Type _type;
    private readonly BaseStrategy _strategy;

    internal FormBuilder(Type type, BaseStrategy strategy)
    {
        _type = type;
        _strategy = strategy;
    }

    /// <summary>
    /// Populate nodes with the value of the <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value with which the nodes will be populated.</param>
    /// <returns>The same instance of the <see cref="FormBuilder"/> for chaining.</returns>
    public FormBuilder EnhanceWithValue(object value)
    {
        if (value != null && !_type.IsInstanceOfType(value))
            throw new ArgumentException("The value type does not match the type the FormBuilder was created for.", nameof(value));

        _strategy.EnhanceWithValue(value);

        return this;
    }

    /// <summary>
    /// Set whether to add validation to the generated data structure.
    /// </summary>
    /// <returns>The same instance of the <see cref="FormBuilder"/> for chaining.</returns>
    public FormBuilder EnhanceWithValidators(bool enhanceWithValidators = true)
    {
        _strategy.Options.EnhanceWithValidators = enhanceWithValidators;

        return this;
    }

    /// <summary>
    /// Build data structure.
    /// </summary>
    /// <returns>The root node of the built data structure.
    /// </returns>
    /// <remarks>
    /// Each returned node is an instance of a <seealso cref="FormControl"/>,
    /// <seealso cref="FormGroup"/> or <seealso cref="FormArray"/>.
    /// </remarks>
    public Node Build()
    {
        return _strategy.Process(_type, new());
    }
}
