using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Sonar.Console.Application;
using Sonar.Console.Infrastructure.CustomerApi;
using Sonar.Console.Infrastructure.Database;
using Sonar.Console.Infrastructure.MetricsApi;
using Xunit;

namespace Sonar.Console.Tests.Application
{
    public class CustomerMetricsProcessorTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IMetricsRepository> _metricsRepository;
        private readonly Mock<IDbRepository> _dbRepository;
        private readonly Mock<ILogger<CustomerMetricsProcessor>> _logger;

        private readonly CustomerMetricsProcessor _sut;

        public CustomerMetricsProcessorTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _metricsRepository = new Mock<IMetricsRepository>();
            _dbRepository = new Mock<IDbRepository>();
            _logger = new Mock<ILogger<CustomerMetricsProcessor>>();

            _sut = new CustomerMetricsProcessor(
                _customerRepository.Object, _metricsRepository.Object,
                _dbRepository.Object,
                _logger.Object);
        }

        [Fact]
        public async Task RunAsync_DataExists_ProcessesRecords()
        {
            //Arrange
            var customers = CustomersFixture.SetupCustomers();
            _customerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);
            _metricsRepository.Setup(x => x.GetAsync(1)).ReturnsAsync(MetricsFixture.Setup(1, 1));
            _metricsRepository.Setup(x => x.GetAsync(2)).ReturnsAsync(MetricsFixture.Setup(3, 2));

            //Act
            await _sut.RunAsync();

            //Assert
            _dbRepository.Verify(x => x.SaveAsync(It.Is<Customer>(c => c.Id == 1)), Times.Once);
            _dbRepository.Verify(x => x.SaveAsync(It.Is<Customer>(c => c.Id == 2)), Times.Once);
            _dbRepository.Verify(x => x.SaveAsync(It.Is<Metric>(m => m.CustomerId == 1)), Times.Exactly(2));
            _dbRepository.Verify(x => x.SaveAsync(It.Is<Metric>(m => m.CustomerId == 2)), Times.Exactly(2));
        }
    }
}
