using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sonar.Console.Infrastructure.CustomerApi;
using Sonar.Console.Infrastructure.MetricsApi;

namespace Sonar.Console
{
    [ExcludeFromCodeCoverage]
    public static class MetricsApiExtensions
    {
        public static IServiceCollection AddMetricsApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IMetricsClient, MetricsClient>(
                MetricsClient.Name,
                (provider, client) =>
                {
                    client.BaseAddress = new Uri(configuration["customerApiBaseUrl"]);
                });

            services.AddScoped<IMetricsRepository, MetricsRepository>();

            return services;
        }
    }
}
