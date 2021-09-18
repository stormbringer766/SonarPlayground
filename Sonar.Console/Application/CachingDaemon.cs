using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sonar.Console.Application
{
    public class CachingDaemon: IHostedService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<CachingDaemon> _logger;

        public CachingDaemon(IServiceProvider services, ILogger<CachingDaemon> logger)
        {
            _services = services;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting");
            // Didn't need to be a hosted service. There was no requirement for it to run more than once.
            using var scope = _services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ICustomerMetricsProcessor>().RunAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Exiting");
            return Task.CompletedTask;
        }
    }
}
