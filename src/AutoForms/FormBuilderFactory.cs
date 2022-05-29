using System.Runtime.CompilerServices;
using AutoForms.FormBuilderStrategies;

[assembly: InternalsVisibleTo("AutoForms.UnitTests")]
namespace AutoForms;

public class FormBuilderFactory
{
    private readonly StrategyResolver _strategyResolver;

    internal FormBuilderFactory(StrategyResolver strategyResolver)
    {
        _strategyResolver = strategyResolver;
    }

    /// <summary>
    /// Create instance of <see cref="FormBuilder"/> for type <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The type for which the data structure is built.</typeparam>
    /// <returns><see cref="FormBuilder"/> without any additional configurations.</returns>
    public FormBuilder CreateFormBuilder<TModel>()
    {
        return CreateFormBuilder(typeof(TModel));
    }

    /// <summary>
    /// Create instance of <see cref="FormBuilder"/> for type <typeparamref name="TModel"/>
    /// and populate nodes with the value of the <paramref name="model"/>.
    /// </summary>
    /// <typeparam name="TModel">The type for which the data structure is built.</typeparam>
    /// <param name="model">The value with which the nodes will be populated.</param>
    /// <returns><see cref="FormBuilder"/> with predefined value.</returns>
    public FormBuilder CreateFormBuilder<TModel>(TModel model)
    {
        return CreateFormBuilder(typeof(TModel))
            .EnhanceWithValue(model);
    }

    /// <summary>
    /// Create instance of <see cref="FormBuilder"/> for type <paramref name="modelType"/>.
    /// </summary>
    /// <param name="modelType">The type for which the data structure is built.</param>
    /// <returns><see cref="FormBuilder"/> without any additional configurations.</returns>
    public FormBuilder CreateFormBuilder(Type modelType)
    {
        var formBuilder = new FormBuilder(modelType, _strategyResolver.Resolve(modelType, new()));

        return formBuilder;
    }
}
