using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.ValueObjects;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.AddStudentCommand;

public class AddStudentCommandHandler(IMediator mediator)
{
    private readonly IMediator mediator = mediator;

    public async Task<Result<Student, Error>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}