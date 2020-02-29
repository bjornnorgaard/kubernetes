using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Infrastructure.HttpExtensions;
using Microsoft.AspNetCore.Http;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Integration.Features.Person
{
    public class CreatePersonTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnBadRequest_WhenAgeIsMissing()
        {
            // Arrange
            var command = new CreatePerson.Command().Invalid();

            // Act
            var result = await Client.PostAsJsonAsync("api/v1/person", command);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ShouldReturnOk_WhenRequestIsValid()
        {
            // Arrange
            var command = new CreatePerson.Command().Valid();

            // Act
            var result = await Client.PostAsJsonAsync("api/v1/person", command);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}



