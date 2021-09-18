using System.Collections.Generic;
using System.Linq;
using Sonar.Console.Infrastructure.CustomerApi;
using Xunit;

namespace Sonar.Console.Tests.Infrastructure.CustomerApi
{
    public class CustomerMapperTests
    {
        [Fact]
        public void Map_CustomerResponseHasValue_ReturnsCustomer()
        {
            //Arrange

            var expected = new CustomerResponse
            {
                id = 1,
                name = "name",
                representative = "rep",
                representative_email = "email",
                representative_phone = "phone"
            };

            var response = new[]
                {
                    expected
                };

            //Act
            var actual = response.Map().First();

            //Assert
            Assert.Equal(expected.id, actual.Id);
            Assert.Equal(expected.name, actual.Name);
            Assert.Equal(expected.representative, actual.Representative.Name);
            Assert.Equal(expected.representative_email, actual.Representative.Email);
            Assert.Equal(expected.representative_phone, actual.Representative.Phone);
        }

        [Fact]
        public void Map_NullResponse_ReturnsNull()
        {
            //Arrange
            //Act
            //Assert
            Assert.Null(((IEnumerable<CustomerResponse>)null).Map());
        }
    }
}
