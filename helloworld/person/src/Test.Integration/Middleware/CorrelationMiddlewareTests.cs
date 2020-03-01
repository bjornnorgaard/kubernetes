using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Test.Integration.Middleware
{
    public class CorrelationMiddlewareTests : TestBase
    {
        private const string HeaderName = "x-correlation-id";

        [Fact]
        public async Task ShouldReturnHeaderWithCorrelationId_WhenNoCorrelationIdIsProvided()
        {
            // Arrange
            Client.DefaultRequestHeaders.Remove(HeaderName);

            // Act
            var result = await Client.GetAsync("api/v1/pet");
            var found = result.Headers.TryGetValues(HeaderName, out var id);

            // Assert
            id.Should().NotBeNullOrEmpty();
            found.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnHeaderWithTheSameCorrelationId_WhenCorrelationIdIsProvided()
        {
            // Arrange
            const string expectation = "im the correlation id";
            Client.DefaultRequestHeaders.Add(HeaderName, expectation);

            // Act
            var result = await Client.GetAsync("api/v1/pet");
            result.Headers.TryGetValues(HeaderName, out var id);

            // Assert
            id.Should().BeEquivalentTo(expectation);
        }
    }
}



