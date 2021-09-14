namespace FormBuilder.Extensions
{
    using FormBuilder.Strategies;
    using Microsoft.Extensions.DependencyInjection;

    public static class FormBuilderExtensions
    {
        public static IServiceCollection AddFormBuilder(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<FormResolver>();

            serviceCollection.AddScoped<BaseStrategy, FormControlStrategy>();
            serviceCollection.AddScoped<BaseStrategy, FormArrayStrategy>();
            serviceCollection.AddScoped<BaseStrategy, FormGroupStrategy>();

            serviceCollection.AddScoped<StrategyResolver>();

            return serviceCollection;
        }
    }
}
