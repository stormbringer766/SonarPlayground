using System.Collections.Generic;
using Sonar.Console.Application;

namespace Sonar.Console.Tests
{
    public static class CustomersFixture
    {
        public static IEnumerable<Customer> SetupCustomers()
        {
            return new[]
            {
                new Customer
                {
                    Id = 1,
                    Name = "customer1",
                    Representative = new Representative
                        { Name = "rep1", Email = "rep1@example.com", Phone = "111-555-1234" }
                },
                new Customer
                {
                    Id = 2,
                    Name = "customer2",
                    Representative = new Representative
                        { Name = "rep2", Email = "rep2@example.com", Phone = "222-555-1234" }
                }
            };
        }
    }
}
