using System;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Infrastructure.HttpExtensions;
using Microsoft.AspNetCore.Http;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Integration.Features.Person
{
    public class UpdatePersonTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnBadRequest_WhenAgeIsMissing()
        {
            // Arrange
            var command = new UpdatePerson.Command().Invalid(Guid.NewGuid());

            // Act
            var result = await Client.PutAsJsonAsync($"api/v1/person/{command.Id}", command);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenIdIdNotFound()
        {
            // Arrange
            var command = new UpdatePerson.Command().Valid(Guid.NewGuid());

            // Act
            var result = await Client.PutAsJsonAsync($"api/v1/person/{command.Id}", command);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task ShouldReturnOk_WhenRequestIsValid()
        {
            // Arrange
            var person = (await Mediator.Send(new CreatePerson.Command().Valid())).Person;
            var command = new UpdatePerson.Command().Valid(person.Id);

            // Act
            var result = await Client.PutAsJsonAsync($"api/v1/person/{command.Id}", command);
            var content = await result.Content.ReadAsAsync<UpdatePerson.Result>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Person.Should().BeEquivalentTo(command);
        }
    }
}



