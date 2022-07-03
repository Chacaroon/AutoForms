using System.Collections;
using System.Collections.Generic;
using AutoForms.Extensions;
using AutoForms.Options;

namespace AutoForms.Resolvers;

internal static class PropertyFormControlTypeResolver
{
    private static readonly Type[] _primitiveTypes =
    {
        typeof(object),
        typeof(string),
        typeof(DateTime)
    };

    internal static bool IsFormControl(FormBuilderContext context)
    {
        return context.ModelType.IsPrimitive
               || context.ModelType.IsEnum
               || _primitiveTypes.Contains(context.ModelType)
               || context.IsFormValue();
    }

    internal static bool IsFormArray(FormBuilderContext context)
    {
        return !IsFormControl(context)
               && !IsDictionary(context)
               && typeof(IEnumerable).IsAssignableFrom(context.ModelType);
    }

    internal static bool IsFormGroup(FormBuilderContext context)
    {
        return !IsFormControl(context) 
               && !IsFormArray(context) 
               && !IsDictionary(context);
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
