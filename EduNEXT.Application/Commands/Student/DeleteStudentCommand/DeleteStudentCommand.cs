using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Student.DeleteStudentCommand;

public class DeleteStudentCommand : IRequest<UnitResult<Error>>
{
    public Guid Id { get; init; }
}