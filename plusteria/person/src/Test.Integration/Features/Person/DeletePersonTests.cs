using System;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Integration.Features.Person
{
    public class DeletePersonTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnNotFound_WhenIdIsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            // Act
            var result = await Client.DeleteAsync($"api/v1/person/{id}");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task ShouldReturnOk_WhenRequestIsValid()
        {
            // Arrange
            var person = (await Mediator.Send(new CreatePerson.Command().Valid())).Person;

            // Act
            var result = await Client.DeleteAsync($"api/v1/person/{person.Id}");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}



