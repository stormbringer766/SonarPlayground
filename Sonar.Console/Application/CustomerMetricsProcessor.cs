using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sonar.Console.Infrastructure.CustomerApi;
using Sonar.Console.Infrastructure.Database;
using Sonar.Console.Infrastructure.MetricsApi;

namespace Sonar.Console.Application
{
    public interface ICustomerMetricsProcessor
    {
        Task RunAsync();
    }
    
    public class CustomerMetricsProcessor: ICustomerMetricsProcessor
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMetricsRepository _metricsRepository;
        private readonly IDbRepository _dbRepository;
        private readonly ILogger<CustomerMetricsProcessor> _logger;

        public CustomerMetricsProcessor(
            ICustomerRepository customerRepository, 
            IMetricsRepository metricsRepository, 
            IDbRepository dbRepository, 
            ILogger<CustomerMetricsProcessor> logger)
        {
            _customerRepository = customerRepository;
            _metricsRepository = metricsRepository;
            _dbRepository = dbRepository;
            _logger = logger;
        }
        
        public async Task RunAsync()
        {
            // If this was a zillion records, this would not work.
            _logger.LogInformation("Getting Customers");
            var customers = await _customerRepository.GetAllAsync();

            _logger.LogInformation("Saving Customers");
            await Task.WhenAll(customers.Select(x => _dbRepository.SaveAsync(x)).ToArray());

            _logger.LogInformation("Getting Metrics");
            var metricTasks = customers.Select(x => _metricsRepository.GetAsync(x.Id)).ToArray();
            await Task.WhenAll(metricTasks);
            
            _logger.LogInformation("Saving Metrics");
            await Task.WhenAll(
                metricTasks.SelectMany(x => x.Result)
                .Select(metric => _dbRepository.SaveAsync(metric))
                .ToArray());

            _logger.LogInformation("Complete");
        }
    }
}
