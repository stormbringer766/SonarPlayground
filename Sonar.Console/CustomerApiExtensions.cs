using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sonar.Console.Infrastructure.CustomerApi;

namespace Sonar.Console
{
    [ExcludeFromCodeCoverage]
    public static class CustomerApiExtensions
    {
        public static IServiceCollection AddCustomerApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICustomerClient, CustomerClient>(
                CustomerClient.Name,
                (provider, client) =>
                {
                    client.BaseAddress = new Uri(configuration["customerApiBaseUrl"]);
                });

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;

        }
    }
}
