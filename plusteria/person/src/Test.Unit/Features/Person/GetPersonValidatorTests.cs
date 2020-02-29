using System;
using Application.Features.Person;
using FluentValidation.TestHelper;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class GetPersonValidatorTests : TestBase
    {
        public GetPersonValidatorTests()
        {
            _uut = new GetPerson.Validator();
        }

        private readonly GetPerson.Validator _uut;

        [Fact]
        public void ShouldHaveValidationError_WhenNameIsInvalid()
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldHaveValidationErrorFor(e => e.Id, Guid.Empty);
        }

        [Fact]
        public void ShouldNotHaveValidationError_WhenNameValid()
        {
            // Arrange

            // Act

            // Assert
            _uut.ShouldNotHaveValidationErrorFor(e => e.Id, Guid.NewGuid());
        }
    }
}



