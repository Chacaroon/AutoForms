namespace AutoForms.Enums;

/// <summary>
/// A validator type
/// </summary>
public enum ValidatorType
{
    /// <summary>
    /// Required validator type
    /// </summary>
    Required = 1,

    /// <summary>
    /// Min length validator type
    /// </summary>
    MinLength = 2,

    /// <summary>
    /// Max length validator type
    /// </summary>
    MaxLength = 3,

    /// <summary>
    /// Regular expression validator type
    /// </summary>
    RegularExpression = 4,
}
