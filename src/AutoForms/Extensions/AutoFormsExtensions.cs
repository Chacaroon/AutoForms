namespace AutoForms.Extensions;

using AutoForms;
using AutoForms.FormBuilderStrategies;
using AutoForms.FormBuilderStrategies.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

public static class AutoFormsExtensions
{
    public static IServiceCollection AddAutoForms(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<FormBuilderFactory>();

        serviceCollection.AddTransient<BaseStrategy, FormControlStrategy>();
        serviceCollection.AddTransient<BaseStrategy, DictionaryFormGroupStrategy>();
        serviceCollection.AddTransient<BaseStrategy, FormArrayStrategy>();
        serviceCollection.AddTransient<BaseStrategy, FormGroupStrategy>();

        serviceCollection.AddTransient<StrategyResolver>();
        serviceCollection.AddSingleton<StrategyOptionsResolver>();

        return serviceCollection;
    }

    public static IMvcBuilder AddAutoFormsSerializer(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

        return mvcBuilder;
    }
}
