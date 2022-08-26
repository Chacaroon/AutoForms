namespace AutoForms.Extensions;

internal static class StringExtensions
{
    internal static string FirstCharToLowerCase(this string str)
    {
        return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str.Substring(1);
    }
}
