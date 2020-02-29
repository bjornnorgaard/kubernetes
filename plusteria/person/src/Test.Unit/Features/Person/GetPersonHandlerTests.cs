using System.Threading;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class GetPersonHandlerTests : TestBase
    {
        public GetPersonHandlerTests()
        {
            _uut = new GetPerson.Handler(Context);
        }

        private readonly GetPerson.Handler _uut;

        [Fact]
        public async Task ShouldReturnPerson_WhenCommandIsValid()
        {
            // Arrange
            var person = await Context.SeedPersonAsync();
            var command = new GetPerson.Command { Id = person.Id };

            // Act
            var result = await _uut.Handle(command, CancellationToken.None);

            // Assert
            result.Person.Should().BeEquivalentTo(person);
        }
    }
}



