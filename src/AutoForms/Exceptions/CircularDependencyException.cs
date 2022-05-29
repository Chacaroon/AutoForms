namespace AutoForms.Exceptions;

/// <summary>
/// Throws when <see cref="FormBuilder"/> builds data structure based on a type with circular references.
/// </summary>
public class CircularDependencyException : Exception
{
    public CircularDependencyException()
    {
    }

    public CircularDependencyException(string message) : base(message)
    {
    }
}
