namespace AutoForms.Attributes;

/// <summary>
/// Specifies that <see cref="FormBuilder"/> converts class, struct or property to a <see cref="Models.FormControl"/> node.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
public class FormValueAttribute : Attribute
{
}
