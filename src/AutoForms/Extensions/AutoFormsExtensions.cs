using AutoForms.FormBuilderStrategies;
using AutoForms.FormBuilderStrategies.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AutoForms.Extensions;

public static class AutoFormsExtensions
{
    /// <summary>
    /// Register services required by AutoForms.
    /// </summary>
    /// <param name="serviceCollection">.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddAutoForms(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(services =>
            new StrategyResolver(
                services.GetRequiredService<IServiceProvider>(),
                services.GetRequiredService<StrategyOptionsResolver>()));

        serviceCollection.AddScoped(services =>
            new FormBuilderFactory(services.GetRequiredService<StrategyResolver>()));

        serviceCollection.AddSingleton(_ => new StrategyOptionsResolver());

        serviceCollection.AddTransient<BaseStrategy, FormControlStrategy>();
        serviceCollection.AddTransient<BaseStrategy, DictionaryFormGroupStrategy>();
        serviceCollection.AddTransient<BaseStrategy, FormArrayStrategy>();
        serviceCollection.AddTransient<BaseStrategy, FormGroupStrategy>();

        return serviceCollection;
    }

    /// <summary>
    /// Register Newtonsoft.Json serializer with predefined settings.
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddAutoFormsSerializer(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

        return mvcBuilder;
    }
}
