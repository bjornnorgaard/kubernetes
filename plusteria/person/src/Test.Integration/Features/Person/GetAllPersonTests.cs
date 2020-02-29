using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Infrastructure.HttpExtensions;
using Microsoft.AspNetCore.Http;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Integration.Features.Person
{
    public class GetAllPersonTests : TestBase
    {
        [Theory]
        [InlineData(10, 0, 10)]
        [InlineData(10, 5, 5)]
        [InlineData(10, 9, 1)]
        [InlineData(10, 10, 0)]
        public async Task ShouldReturnOne_WhenTakeIs1AndSkipIs1(int take,
                                                                int skip,
                                                                int expectedCount)
        {
            // Arrange
            var persons = new List<Models.Person>();
            for (var i = 0; i < 10; i++)
                persons.Add((await Mediator.Send(new CreatePerson.Command().Valid())).Person);

            // Act
            var result = await Client.GetAsync($"api/v1/person?take={take}&skip={skip}");
            var content = await result.Content.ReadAsAsync<GetAllPerson.Result>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Persons.Count().Should().Be(expectedCount);
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenTakeIs0()
        {
            // Arrange

            // Act
            var result = await Client.GetAsync("api/v1/person?take=0");

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ShouldReturnList_WhenRequestIsValid()
        {
            // Arrange

            // Act
            var result = await Client.GetAsync("api/v1/person");
            var content = await result.Content.ReadAsAsync<GetAllPerson.Result>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            content.Should().BeOfType<GetAllPerson.Result>();
        }
    }
}



