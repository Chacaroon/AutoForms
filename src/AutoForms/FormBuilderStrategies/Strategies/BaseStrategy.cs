using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AutoForms.Enums;
using AutoForms.Exceptions;
using AutoForms.Models;
using AutoForms.Options;

namespace AutoForms.FormBuilderStrategies.Strategies;

internal abstract class BaseStrategy
{
    private protected object Value { get; private set; }

    private protected Models.Validator[] Validators { get; private set; }

    internal StrategyOptions Options { get; private set; } = new();

    #region AbstractMethods

    internal abstract bool IsStrategyApplicable(Type modelType, ResolvingStrategyOptions options);

    internal abstract Node Process(Type type, HashSet<Type> hashSet);

    #endregion

    internal BaseStrategy EnhanceWithValue(object value)
    {
        Value = value;

        return this;
    }

    internal BaseStrategy EnhanceWithValidators(PropertyInfo propertyInfo)
    {
        if (!Options.EnhanceWithValidators)
            return this;

        Validators = EnhanceWithValidators(propertyInfo.CustomAttributes)
            .Union(EnhanceWithValidators(propertyInfo.PropertyType.CustomAttributes))
            .ToArray();

        return this;
    }

    internal BaseStrategy PopulateOptions(StrategyOptions options)
    {
        Options = options;

        return this;
    }

    #region Helpers

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

    #endregion

    private Models.Validator[] EnhanceWithValidators(IEnumerable<CustomAttributeData> attributes)
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
