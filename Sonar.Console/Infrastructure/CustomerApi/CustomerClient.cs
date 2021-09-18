using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sonar.Console.Infrastructure.CustomerApi
{
    public interface ICustomerClient
    {
        Task<T> GetAsync<T>(string url);
    }
    public class CustomerClient: ICustomerClient
    {
        public const string Name = "CustomerClient";
        private readonly HttpClient _client;

        public CustomerClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content.FromJson<T>();
            }

            //Throw?
            return default;
        }
    }
}
