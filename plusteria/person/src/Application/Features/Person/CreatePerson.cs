using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Repository;

namespace Application.Features.Person
{
    public class CreatePerson
    {
        public class Command : IRequest<Result>
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Result
        {
            public Models.Person Person { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(r => r.Name).NotEmpty();
                RuleFor(r => r.Age).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IContext _context;
            private readonly IMapper _mapper;

            public Handler(IContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = _mapper.Map<Models.Person>(request);

                await _context.Set<Models.Person>().AddAsync(person, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var result = new Result { Person = person };

                return result;
            }
        }
    }
}



