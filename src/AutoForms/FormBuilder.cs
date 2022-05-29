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

    public FormBuilder EnhanceWithValue(object value)
    {
        if (value != null && !value.GetType().IsAssignableTo(_type))
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

    public Node Build()
    {
        return _strategy.Process(_type, new(new TypeEqualityComparer()));
    }
}
