namespace AutoForms.Models
{
    using AutoForms.Enums;

    public class Validator
    {
        public Validator(ValidatorType type)
        {
            Type = type;
        }

        public ValidatorType Type { get; }

        public virtual object Value { get; set; }
    }
}
