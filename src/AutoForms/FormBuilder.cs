using AutoForms.Comparers;
using AutoForms.FormBuilderStrategies.Strategies;
using AutoForms.Models;

namespace AutoForms;

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
    /// Populate nodes with the value of the <paramref name="value"/>
    /// </summary>
    /// <param name="value">The value with which the nodes will be populated</param>
    /// <returns>The same instance of the <see cref="FormBuilder"/> for chaining.</returns>
    public FormBuilder EnhanceWithValue(object value)
    {
        _strategy.EnhanceWithValue(value);

        return this;
    }

    /// <summary>
    /// Populate nodes with the validators based on validation attributes/>
    /// </summary>
    /// <returns>The same instance of the <see cref="FormBuilder"/> for chaining.</returns>
    {
        _strategy.EnhanceWithValidators(_type);

        return this;
    }

    /// <summary>
    /// Build data structure
    /// </summary>
    /// <returns>The root <see cref="Node"/> of the built data structure.
    /// Each node is an instance of a <seealso cref="FormControl"/>, <seealso cref="FormGroup"/> or <seealso cref="FormArray"/>
    /// </returns>
    public Node Build()
    {
        return _strategy.Process(_type, new(new TypeEqualityComparer()));
    }
}
