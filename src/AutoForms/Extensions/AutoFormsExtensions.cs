using AutoForms.Processors;
using AutoForms.Resolvers;
using AutoForms.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AutoForms.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>
/// </summary>
[ExcludeFromCodeCoverage]
public static class AutoFormsExtensions
{
    /// <summary>
    /// Register services required by AutoForms.
    /// </summary>
    /// <param name="serviceCollection">.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddAutoForms(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(services =>
            new StrategyResolver(services.GetRequiredService<IServiceProvider>()));

        serviceCollection.AddScoped(services =>
            new FormBuilderFactory(services.GetRequiredService<StrategyResolver>()));

        var strategies = Assembly.GetAssembly(typeof(BaseStrategy))!
            .GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => typeof(BaseStrategy).IsAssignableFrom(x));

        foreach (var strategyType in strategies)
        {
            serviceCollection.AddSingleton(typeof(BaseStrategy), strategyType);
        }

        return serviceCollection;
    }

    /// <summary>
    /// Register ControlProcessor that processes validation attributes
    /// </summary>
    /// <param name="serviceCollection">.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddValidationProcessor(this IServiceCollection serviceCollection)
    {
        var processors = Assembly.GetAssembly(typeof(BaseControlProcessor))!
            .GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => typeof(BaseControlProcessor).IsAssignableFrom(x));

        foreach (var processorType in processors)
        {
            serviceCollection.AddSingleton(typeof(BaseControlProcessor), processorType);
        }

        return serviceCollection;
    }

    /// <summary>
    /// Register ControlProcessors
    /// </summary>
    /// <param name="serviceCollection">.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddFormBuilderProcessors(this IServiceCollection serviceCollection)
    {
        AddFormBuilderProcessors(serviceCollection, Assembly.GetCallingAssembly());

        return serviceCollection;
    }

    /// <summary>
    /// Register ControlProcessors
    /// </summary>
    /// <param name="serviceCollection">.</param>
    /// <param name="assembly">Assembly in which to look for control processors</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddFormBuilderProcessors(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var processors = assembly
            .GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => typeof(BaseControlProcessor).IsAssignableFrom(x));

        foreach (var processorType in processors)
        {
            serviceCollection.AddSingleton(typeof(BaseControlProcessor), processorType);
        }

        return serviceCollection;
    }

    /// <summary>
    /// Register Newtonsoft.Json serializer with predefined settings.
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <returns><see cref="IMvcBuilder"/>.</returns>
    [ExcludeFromCodeCoverage]
    public static IMvcBuilder AddAutoFormsSerializer(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

        return mvcBuilder;
    }

    /// <summary>
    /// Register Newtonsoft.Json serializer with predefined settings.
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="action">Callback to configure <see cref="MvcNewtonsoftJsonOptions"/>.</param>
    /// <returns><see cref="IMvcBuilder"/></returns>
    [ExcludeFromCodeCoverage]
    public static IMvcBuilder AddAutoFormsSerializer(this IMvcBuilder mvcBuilder, Action<MvcNewtonsoftJsonOptions> action)
    {
        mvcBuilder.AddNewtonsoftJson(options =>
        {
            action(options);
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

        return mvcBuilder;
    }
}
