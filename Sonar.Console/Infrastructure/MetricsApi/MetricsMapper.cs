using System.Collections.Generic;
using System.Linq;
using Sonar.Console.Application;

namespace Sonar.Console.Infrastructure.MetricsApi
{
    public static class MetricsMapper
    {
        public static IEnumerable<Metric> Map(this IEnumerable<MetricsResponse> response)
        {
            return response?.Select(metric => new Metric
            {
                Id = metric.id,
                CustomerId = metric.customer_id,
                Name = metric.name,
                Expression = metric.expression
            });
        }
    }
}
