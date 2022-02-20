namespace AutoForms.Helpers
{
    using AutoForms.Options;
    using System;
    using System.Collections;
    using System.Linq;

    internal static class PropertyFormControlTypeResolver
    {
        private static readonly Type[] PrimitiveTypes =
        {
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
            return !IsFormControl(type, options) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        internal static bool IsFormGroup(Type type, StrategyOptions options)
        {
            return !IsFormControl(type, options) && !IsFormArray(type, options);
        }
    }
}
