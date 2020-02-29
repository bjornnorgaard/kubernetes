using System;
using Application.Features.Person;

namespace Test.Arrange.Features.Person
{
    public static class UpdatePersonHelper
    {
        public static UpdatePerson.Command Valid(this UpdatePerson.Command command, Guid id)
        {
            return new UpdatePerson.Command { Id = id, Name = "John Doe", Age = 42 };
        }

        public static UpdatePerson.Command Invalid(this UpdatePerson.Command command, Guid id)
        {
            return new UpdatePerson.Command { Id = id, Name = "John Doe" };
        }
    }
}



