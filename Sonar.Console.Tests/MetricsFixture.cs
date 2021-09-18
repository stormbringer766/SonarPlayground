using System.Collections.Generic;
using Sonar.Console.Application;

namespace Sonar.Console.Tests
{
    public static class MetricsFixture
    {
        public static IEnumerable<Metric> Setup(int idStart, int customerId)
        {

            yield return new Metric
            {
                Id = idStart,
                CustomerId = customerId,
                Name = "metric" + idStart,
                Expression = "expr" + idStart
            };

            yield return new Metric
            {
                Id = idStart++,
                CustomerId = customerId,
                Name = "metric" + idStart,
                Expression = "expr" + idStart
            };
        }
    }
}
