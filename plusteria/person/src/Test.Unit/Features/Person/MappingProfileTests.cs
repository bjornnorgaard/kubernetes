using System;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Test.Unit.Features.Person
{
    public class MappingProfileTests : TestBase
    {
        [Fact]
        public void ShouldNotThrowConfigurationException_WhenMapIsConfigured()
        {
            // Arrange
            var configuration = Mapper.ConfigurationProvider;

            // Act
            Action act = () => configuration.AssertConfigurationIsValid();

            // Assert
            act.Should().NotThrow<AutoMapperConfigurationException>();
        }
    }
}



