namespace AutoForms;

using System.Collections.Generic;
using AutoForms.Comparers;
using AutoForms.FormBuilderStrategies.Strategies;
using AutoForms.Models;

public class FormBuilder
{
    private readonly Type _type;
    private readonly BaseStrategy _strategy;

    public FormBuilder(Type type, BaseStrategy strategy)
    {
        _type = type;
        _strategy = strategy;
    }

    public FormBuilder EnhanceWithValue(object value)
    {
        _strategy.EnhanceWithValue(value);

        return this;
    }

    public FormBuilder EnhanceWithValidators()
    {
        _strategy.EnhanceWithValidators(_type);

        return this;
    }

    public Node Build()
    {
        return _strategy.Process(_type, new(new TypeEqualityComparer()));
    }
}
