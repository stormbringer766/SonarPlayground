using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sonar.Console.Application;
using Sonar.Console.Infrastructure.CustomerApi;

namespace Sonar.Console.Infrastructure.MetricsApi
{
    public interface IMetricsRepository
    {
        public Task<IEnumerable<Metric>> GetAsync(int customerId);
    }

    public class MetricsRepository: IMetricsRepository
    {
        private readonly IMetricsClient _client;

        public MetricsRepository(IMetricsClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Metric>> GetAsync(int customerId)
        {
            var response = await _client.GetAsync<IEnumerable<MetricsResponse>>($"sonar-apitest-metrics?customer_id={customerId}");
            return response.Map();
        }
    }
}
