namespace AutoForms.Models;

using AutoForms.Enums;

public class Validator
{
    public Validator(ValidatorType type)
    {
        Type = type;
    }

    public ValidatorType Type { get; }

    public string Message { get; set; }

    public virtual object Value { get; set; }
}
