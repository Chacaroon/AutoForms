namespace AutoForms.Extensions
{
    using AutoForms;
    using AutoForms.FormResolverStrategies;
    using AutoForms.FormResolverStrategies.Strategies;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class AutoFormsExtensions
    {
        public static IServiceCollection AddAutoForms(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<FormResolver>();

            serviceCollection.AddScoped<BaseStrategy, FormControlStrategy>();
            serviceCollection.AddScoped<BaseStrategy, FormArrayStrategy>();
            serviceCollection.AddScoped<BaseStrategy, FormGroupStrategy>();

            serviceCollection.AddScoped<StrategyResolver>();
            serviceCollection.AddScoped<StrategyOptionsResolver>();

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
}
