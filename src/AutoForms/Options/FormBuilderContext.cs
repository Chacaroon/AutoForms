using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoForms.Attributes;

namespace AutoForms.Options;

/// <summary>
/// Context that contains information for building FormControl
/// </summary>
public record FormBuilderContext(Type ModelType)
{
    public object? Value { get; set; }

    public bool EnhanceWithValidators { get; init; }

    public PropertyInfo? PropertyInfo { get; init; }

    public bool IsFormValue() => GetAttributes().Any(x => x.GetType() == typeof(FormValueAttribute));

    public Attribute[] GetAttributes() =>
        ModelType
            .GetCustomAttributes()
            .Concat(PropertyInfo?.GetCustomAttributes() ?? Array.Empty<Attribute>())
            .ToArray();
}
