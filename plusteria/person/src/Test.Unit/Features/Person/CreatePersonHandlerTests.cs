using System.Threading;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class CreatePersonHandlerTests : TestBase
    {
        public CreatePersonHandlerTests()
        {
            _uut = new CreatePerson.Handler(Context, Mapper);
        }

        private readonly CreatePerson.Handler _uut;

        [Fact]
        public async Task ShouldReturnPerson_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreatePerson.Command().Valid();

            // Act
            var result = await _uut.Handle(command, CancellationToken.None);

            // Assert
            result.Person.Id.Should().NotBeEmpty();
        }
    }
}



