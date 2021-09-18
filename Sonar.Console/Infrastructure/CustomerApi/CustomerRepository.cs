using System.Collections.Generic;
using System.Threading.Tasks;
using Sonar.Console.Application;

namespace Sonar.Console.Infrastructure.CustomerApi
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetAllAsync();
    }

    public class CustomerRepository: ICustomerRepository
    {
        private readonly ICustomerClient _client;

        public CustomerRepository(ICustomerClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var response = await _client.GetAsync<IEnumerable<CustomerResponse>>("sonar-apitest-customer");
            return response.Map();
        }
    }
}
