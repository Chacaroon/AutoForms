namespace FormBuilder.Helpers
{
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

        internal static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive
                   || type.IsEnum
                   || PrimitiveTypes.Contains(type);
        }

        internal static bool IsCollection(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        internal static bool IsFormGroup(Type type)
        {
            return !IsPrimitive(type) && !IsCollection(type);
        }
    }
}
