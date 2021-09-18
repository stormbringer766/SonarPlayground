using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sonar.Console.Application;

namespace Sonar.Console
{
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        public static void Configure(HostBuilderContext context, IConfigurationBuilder configurationBuilder)
        {
            Configuration = configurationBuilder
                .AddJsonFile("appsettings.json", optional: false, true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static void ConfigureService(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddSingleton<IHostedService, CachingDaemon>()
                .AddCustomerApi(Configuration)
                .AddMetricsApi(Configuration)
                .AddDatabase(Configuration)
                .AddScoped<ICustomerMetricsProcessor, CustomerMetricsProcessor>();

        }
        private static IConfiguration Configuration { get; set; }
    }
}
