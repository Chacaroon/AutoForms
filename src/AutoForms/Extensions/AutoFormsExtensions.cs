using AutoForms.FormBuilderStrategies;
using AutoForms.FormBuilderStrategies.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AutoForms.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>
/// </summary>
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

        var strategies = Assembly.GetAssembly(typeof(BaseStrategy))!
            .GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => typeof(BaseStrategy).IsAssignableFrom(x));

        foreach (var strategyType in strategies)
        {
            serviceCollection.AddTransient(typeof(BaseStrategy), strategyType);
        }

        return serviceCollection;
    }
}
