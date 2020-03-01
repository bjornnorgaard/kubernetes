using System.Threading;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class UpdatePersonHandlerTests : TestBase
    {
        public UpdatePersonHandlerTests()
        {
            _uut = new UpdatePerson.Handler(Context, Mapper);
        }

        private readonly UpdatePerson.Handler _uut;

        [Fact]
        public async Task ShouldReturnUpdatedPerson_WhenCommandIsValid()
        {
            // Arrange
            var person = await Context.SeedPersonAsync();
            var command = new UpdatePerson.Command().Valid(person.Id);

            // Act
            var result = await _uut.Handle(command, CancellationToken.None);

            // Assert
            result.Person.Should().BeEquivalentTo(command);
        }
    }
}



