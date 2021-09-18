using System.Collections.Generic;
using System.Linq;
using Sonar.Console.Application;

namespace Sonar.Console.Infrastructure.CustomerApi
{
    public static class CustomerMapper
    {
        public static IEnumerable<Customer> Map(this IEnumerable<CustomerResponse> response)
        {
            return response?.Select(customer => new Customer
            {
                Id = customer.id,
                Name = customer.name,
                Representative = new Representative
                {
                    Name = customer.representative,
                    Email = customer.representative_email,
                    Phone = customer.representative_phone
                }
            });
        }
    }
}
