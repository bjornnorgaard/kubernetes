using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Test.Integration.Middleware
{
    public class HealthCheckTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnOkAndHealthy_WhenEndpointIsCalled()
        {
            // Arrange

            // Act
            var result = await Client.GetAsync("/health");
            var content = await result.Content.ReadAsStringAsync();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Should().Contain("Healthy");
        }
    }
}



