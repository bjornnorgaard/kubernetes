using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Application.Features.Person
{
    public class UpdatePerson
    {
        public class Command : IRequest<Result>
        {
            public Guid Id { get; set; }
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
                RuleFor(r => r.Age).GreaterThan(0).NotEmpty();
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
                var person = await _context.Set<Models.Person>()
                                           .FirstOrDefaultAsync(p => p.Id == request.Id,
                                                                cancellationToken);

                if (person == null) throw new NotFoundException(request.Id, typeof(Models.Person));

                person = _mapper.Map(request, person);

                await _context.SaveChangesAsync(cancellationToken);

                var result = new Result { Person = person };

                return result;
            }
        }
    }
}



