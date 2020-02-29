using Application.Features.Person;
using FluentValidation.TestHelper;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class GetAllPersonValidatorTests : TestBase
    {
        private readonly GetAllPerson.Validator _uut;

        public GetAllPersonValidatorTests()
        {
            _uut = new GetAllPerson.Validator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldHaveValidationError_WhenNameIsInvalid(int take)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldHaveValidationErrorFor(e => e.Take, take);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ShouldNotHaveValidationError_WhenNameValid(int take)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldNotHaveValidationErrorFor(e => e.Take, take);
        }
    }
}



