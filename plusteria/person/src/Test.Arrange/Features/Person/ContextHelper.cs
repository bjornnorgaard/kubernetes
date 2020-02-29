using System.Threading.Tasks;
using Repository;

namespace Test.Arrange.Features.Person
{
    public static class ContextHelper
    {
        public static async Task<Models.Person> SeedPersonAsync(this Context context,
                                                                Models.Person model = null)
        {
            if (model == null) model = new Models.Person { Name = "John Doe", Age = 42 };

            await context.Persons.AddAsync(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}



