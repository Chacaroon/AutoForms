using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AutoForms.Enums;
using AutoForms.Exceptions;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

public abstract class BaseStrategy
{
    private protected object Value { get; private set; }

    private protected Models.Validator[] Validators { get; private set; }

    internal abstract bool IsStrategyApplicable(Type modelType, StrategyOptions options);

    internal abstract Node Process(Type type, HashSet<Type> hashSet);

    protected void CheckCircularDependency(ref HashSet<Type> hashSet, Type type)
    {
        if (hashSet.Contains(type))
        {
            var typesArray = hashSet.Concat(new[] { type }).ToArray();
            typesArray = typesArray[Array.IndexOf(typesArray, type)..];
            var path = string.Join("->", typesArray.Select(x => x.Name));
            throw new CircularDependencyException($"Circular dependency: {path}");
        }

        hashSet = hashSet.Union(new[] { type }).ToHashSet(hashSet.Comparer);
    }
    
    internal BaseStrategy EnhanceWithValue(object value)
    {
        Value = value;

        return this;
    }

    internal BaseStrategy EnhanceWithValidators(PropertyInfo propertyInfo)
    {
        Validators = ResolveValidators(propertyInfo.CustomAttributes)
            .Union(ResolveValidators(propertyInfo.PropertyType.CustomAttributes))
            .ToArray();

        return this;
    }

    internal BaseStrategy EnhanceWithValidators(Type type)
    {
        Validators = ResolveValidators(type.CustomAttributes);

        return this;
    }

    private Models.Validator[] ResolveValidators(IEnumerable<CustomAttributeData> attributes)
    {
        var validatorsDictionary = new Dictionary<Type, Func<CustomAttributeData, Models.Validator>>
        {
            {
                typeof(RequiredAttribute), _ => new Models.Validator(ValidatorType.Required)
                {
                    Message = "This field is required"
                }
            },
            {
                typeof(MinLengthAttribute),
                attributeData => new Models.Validator(ValidatorType.MinLength)
                {
                    Value = attributeData.ConstructorArguments.First().Value,
                    Message = $"Min length is {attributeData.ConstructorArguments.First().Value}"
                }
            },
            {
                typeof(MaxLengthAttribute),
                attributeData => new Models.Validator(ValidatorType.MaxLength)
                {
                    Value = attributeData.ConstructorArguments.First().Value,
                    Message = $"Max length is {attributeData.ConstructorArguments.First().Value}"
                }
            }
        };

        return attributes
            .Select(x => validatorsDictionary.TryGetValue(x.AttributeType, out var func) ? func(x) : null)
            .Where(x => x != null)
            .ToArray();
    }
}
