using Application.Features.Person;

namespace Test.Arrange.Features.Person
{
    public static class CreatePersonHelper
    {
        public static CreatePerson.Command Valid(this CreatePerson.Command command)
        {
            return new CreatePerson.Command { Name = "John Doe", Age = 42 };
        }

        public static CreatePerson.Command Invalid(this CreatePerson.Command command)
        {
            return new CreatePerson.Command { Name = "John Doe" };
        }
    }
}



