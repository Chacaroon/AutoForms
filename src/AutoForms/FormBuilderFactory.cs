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

    public FormBuilder CreateFormBuilder<TModel>()
    {
        return CreateFormBuilder(typeof(TModel));
    }

    public FormBuilder CreateFormBuilder<TModel>(TModel model)
    {
        return CreateFormBuilder(typeof(TModel))
            .EnhanceWithValue(model);
    }

    public FormBuilder CreateFormBuilder(Type modelType)
    {
        var formBuilder = new FormBuilder(modelType, _strategyResolver.Resolve(modelType, new()));

        return formBuilder;
    }
}
