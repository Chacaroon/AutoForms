namespace AutoForms.FormBuilderStrategies.Strategies;

using AutoForms.Helpers;
using AutoForms.Models;
using AutoForms.Options;
using System.Collections;
using System.Collections.Generic;

internal class FormArrayStrategy : BaseStrategy
{
    private readonly FormBuilderFactory _formBuilderFactory;

    public FormArrayStrategy(FormBuilderFactory formBuilderFactory)
    {
        _formBuilderFactory = formBuilderFactory;
    }

    internal override bool IsStrategyApplicable(Type modelType, StrategyOptions options)
    {
        return !PropertyFormControlTypeResolver.IsFormControl(modelType, options)
               && PropertyFormControlTypeResolver.IsFormArray(modelType, options);
    }

    internal override Node Process(Type type)
    {
        var collectionItemType = GetCollectionItemType(type);
        var values = ((IEnumerable)Value)?.Cast<object>() ?? Array.Empty<object>();

        Node BuildNode(object value) => _formBuilderFactory.CreateFormBuilder(collectionItemType)
            .EnhanceWithValidators()
            .EnhanceWithValue(value)
            .Build();

        var nodes = values.Select(BuildNode);

        var formArray = new FormArray
        {
            Nodes = nodes,
            NodeSchema = BuildNode(null)
        };

        return formArray;
    }

    private Type GetCollectionItemType(Type collectionType)
    {
        var interfaces = collectionType.GetInterfaces().Union(new[] { collectionType });

        var enumerableInterfaceType = interfaces.First(x =>
            x.IsGenericType
            && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        var collectionItemType = enumerableInterfaceType
            .GetGenericArguments()
            .Single();

        return collectionItemType;
    }
}
