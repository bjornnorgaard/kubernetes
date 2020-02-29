using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Test.Integration.Swagger
{
    public class SwaggerTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnOk_WhenSwaggerDocsWorks()
        {
            // Arrange

            // Act
            var result = await Client.GetAsync("swagger/v1/swagger.json");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}



