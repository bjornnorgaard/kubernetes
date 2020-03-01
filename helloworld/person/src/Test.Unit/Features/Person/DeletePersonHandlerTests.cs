using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Person;
using FluentAssertions;
using Test.Arrange.Features.Person;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class DeletePersonHandlerTests : TestBase
    {
        public DeletePersonHandlerTests()
        {
            _uut = new DeletePerson.Handler(Context);
        }

        private readonly DeletePerson.Handler _uut;

        [Fact]
        public async Task ShouldReturnUpdatedPerson_WhenCommandIsValid()
        {
            // Arrange
            var person = await Context.SeedPersonAsync();
            var command = new DeletePerson.Command { Id = person.Id };

            // Act
            await _uut.Handle(command, CancellationToken.None);

            // Assert
            var expectedToBeNull = Context.Persons.FirstOrDefault(p => p.Id == person.Id);
            expectedToBeNull.Should().BeNull();
        }
    }
}



