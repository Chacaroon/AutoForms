using AutoForms.Enums;
using AutoForms.Models;
using AutoForms.Options;
using System.ComponentModel.DataAnnotations;

namespace AutoForms.Processors;

internal class ValidationProcessor : BaseControlProcessor
{
    public override void Process(AbstractControl control, FormBuilderContext context)
    {
        if (control.Type != ControlType.Control)
        {
            return;
        }

        var validators = context.GetAttributes()
            .Select(x => ProcessValidator(x)!)
            .Where(x => x != null)
            .ToArray();

        control.Validators = validators;
    }

    private Models.Validator? ProcessValidator(Attribute attribute)
    {
        return attribute switch
        {
            RequiredAttribute requiredAttribute => new Models.Validator(ValidatorType.Required)
            {
                Message = requiredAttribute.ErrorMessage
            },
            MinLengthAttribute minLengthAttribute => new Models.Validator(ValidatorType.MinLength)
            {
                Message = minLengthAttribute.ErrorMessage,
                Value = minLengthAttribute.Length
            },
            MaxLengthAttribute maxLengthAttribute => new Models.Validator(ValidatorType.MaxLength)
            {
                Message = maxLengthAttribute.ErrorMessage,
                Value = maxLengthAttribute
            },
            RegularExpressionAttribute regularExpressionAttribute => new Models.Validator(ValidatorType.RegularExpression)
            {
                Message = regularExpressionAttribute.ErrorMessage,
                Value = regularExpressionAttribute.Pattern
            },
            _ => null
        };
    }
}
