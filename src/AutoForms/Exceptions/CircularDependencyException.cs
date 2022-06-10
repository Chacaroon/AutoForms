namespace AutoForms.Exceptions;

/// <summary>
/// Throws when <see cref="FormBuilder"/> builds data structure based on a type with circular references.
/// </summary>
public class CircularDependencyException : Exception
{
    /// <summary>
    /// Create the <see cref="CircularDependencyException"/> with message
    /// </summary>
    /// <param name="message">The exception message</param>
    public CircularDependencyException(string message) : base(message)
    {
    }
}
