using System.Collections;
using System.Collections.Generic;
using AutoForms.Options;

namespace AutoForms.Resolvers;

internal static class PropertyFormControlTypeResolver
{
    private static readonly Type[] PrimitiveTypes =
    {
        typeof(object),
        typeof(string),
        typeof(DateTime)
    };

    internal static bool IsFormControl(FormBuilderContext options)
    {
        return options.ModelType.IsPrimitive
               || options.ModelType.IsEnum
               || PrimitiveTypes.Contains(options.ModelType)
               || options.IsFormValue();
    }

    internal static bool IsFormArray(FormBuilderContext context)
    {
        return !IsFormControl(context)
               && !IsDictionary(context)
               && typeof(IEnumerable).IsAssignableFrom(context.ModelType);
    }

    internal static bool IsFormGroup(FormBuilderContext context)
    {
        return !IsFormControl(context) && !IsFormArray(context) && !IsDictionary(context);
    }

    internal static bool IsDictionary(FormBuilderContext context)
    {
        return !IsFormControl(context)
               && context.ModelType.IsGenericType
               && context.ModelType.GetInterfaces()
                   .Concat(new[] { context.ModelType })
                   .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
    }
}
