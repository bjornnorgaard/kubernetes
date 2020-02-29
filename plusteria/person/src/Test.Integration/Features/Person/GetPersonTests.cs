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
    public class GetPersonTests : TestBase
    {
        [Fact]
        public async Task ShouldReturn404_WhenIdIsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();

            // Act
            var result = await Client.GetAsync($"api/v1/person/{id}");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task ShouldReturnPersonAndOk_WhenExistsWithId()
        {
            // Arrange
            var expectation = (await Mediator.Send(new CreatePerson.Command().Valid())).Person;

            // Act
            var result = await Client.GetAsync($"api/v1/person/{expectation.Id}");
            var content = await result.Content.ReadAsAsync<GetPerson.Result>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Person.Should().BeEquivalentTo(expectation);
        }
    }
}



