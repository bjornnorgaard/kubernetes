using Application.Features.Person;
using FluentValidation.TestHelper;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class UpdatePersonValidatorTests : TestBase
    {
        private readonly UpdatePerson.Validator _uut;

        public UpdatePersonValidatorTests()
        {
            _uut = new UpdatePerson.Validator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldHaveValidationError_WhenNameIsInvalid(string name)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldHaveValidationErrorFor(e => e.Name, name);
        }

        [Theory]
        [InlineData("Kenny")]
        [InlineData("K")]
        public void ShouldNotHaveValidationError_WhenNameValid(string name)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldNotHaveValidationErrorFor(e => e.Name, name);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ShouldHaveValidationError_WhenAgeIsInvalid(int age)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldHaveValidationErrorFor(e => e.Age, age);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldNotHaveValidationError_WhenAgeIsValid(int age)
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldNotHaveValidationErrorFor(e => e.Age, age);
        }
    }
}



