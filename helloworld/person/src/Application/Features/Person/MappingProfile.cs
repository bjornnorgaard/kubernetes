using AutoMapper;

namespace Application.Features.Person
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePerson.Command, Models.Person>().ForMember(d => d.Id, o => o.Ignore());
            CreateMap<UpdatePerson.Command, Models.Person>();
        }
    }
}



