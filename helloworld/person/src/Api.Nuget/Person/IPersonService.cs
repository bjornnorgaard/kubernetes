using System;
using System.Threading.Tasks;
using Application.Features.Person;
using RestEase;

namespace Api.Nuget.Person
{
    [Header("User-Agent", "RestEase")]
    [Header("Accept", "application/json")]
    public interface IPersonService
    {
        [Get("/api/v1/person")]
        Task<GetPerson.Result> GetAllPersonAsync([Query] int take, [Query] int skip);

        [Get("/api/v1/person/{id}")]
        Task<GetPerson.Result> GetPersonAsync([Path] Guid id);

        [Post("/api/v1/person")]
        Task<CreatePerson.Result> CreatePersonAsync([Body] CreatePerson.Command command);

        [Put("/api/v1/person/{id}")]
        Task<UpdatePerson.Result> UpdatePersonAsync([Path] Guid id,
                                                    [Body] UpdatePerson.Command command);

        [Delete("/api/v1/person/{id}")]
        Task<DeletePerson.Result> DeletePersonAsync([Path] Guid id);
    }
}



