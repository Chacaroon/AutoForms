using AutoForms.Attributes;
using AutoForms.Options;

namespace AutoForms.Extensions;

/// <summary>
/// Extensions for <see cref="FormBuilderContext"/>
/// </summary>
public static class FormBuilderContextExtensions
{
    private static readonly Type _formValueAttributeType = typeof(FormValueAttribute);

    /// <summary>
    /// Determines if the current node has a <see cref="FormValueAttribute"/>
    /// </summary>
    /// <param name="context"></param>
    /// <returns><see langword="true"/> if the current node has the <see cref="FormValueAttribute"/>;
    /// otherwise <see langword="false"/></returns>
    public static bool IsFormValue(this FormBuilderContext context)
    {
        return context.GetAttributes().Any(x => x.GetType() == _formValueAttributeType);
    }
}