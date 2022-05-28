using System.Collections;
using System.Collections.Generic;
using AutoForms.Options;

namespace AutoForms.Helpers;

internal static class PropertyFormControlTypeResolver
{
    private static readonly Type[] PrimitiveTypes =
    {
        typeof(object),
        typeof(string),
        typeof(DateTime)
    };

    internal static bool IsFormControl(Type type, StrategyOptions options)
    {
        return type.IsPrimitive
               || type.IsEnum
               || PrimitiveTypes.Contains(type)
               || options.IsFormValue;
    }

    internal static bool IsFormArray(Type type, StrategyOptions options)
    {
        return !IsFormControl(type, options)
               && !IsDictionary(type, options)
               && typeof(IEnumerable).IsAssignableFrom(type);
    }

    internal static bool IsFormGroup(Type type, StrategyOptions options)
    {
        return !IsFormControl(type, options) && !IsFormArray(type, options);
    }

    internal static bool IsDictionary(Type type, StrategyOptions options)
    {
        return !IsFormControl(type, options)
               && type.IsGenericType
               && type.GetInterfaces()
                   .Concat(new[] { type })
                   .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));
    }
}
