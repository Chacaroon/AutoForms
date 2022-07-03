using System.Reflection;

namespace AutoForms.Options;

/// <summary>
/// Context that contains information for building FormControl
/// </summary>
/// <param name="ModelType">Type of processed model</param>
public record FormBuilderContext(Type ModelType)
{
    /// <summary>
    /// The current node's value
    /// </summary>
    public object? Value { get; init; }

    /// <summary>
    /// The <see cref="System.Reflection.PropertyInfo"/> data of the processed field
    /// </summary>
    public PropertyInfo? PropertyInfo { get; init; }

    /// <summary>
    /// Returns all ModelType and PropertyInfo (if present) attributes
    /// </summary>
    /// <returns>Array with attributes</returns>
    public Attribute[] GetAttributes() =>
        ModelType
            .GetCustomAttributes()
            .Concat(PropertyInfo?.GetCustomAttributes() ?? Array.Empty<Attribute>())
            .ToArray();
}
