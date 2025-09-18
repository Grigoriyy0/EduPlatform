using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.DeleteStudentCommand;

public class DeleteStudentCommand : IRequest<UnitResult<Error>>
{
    public Guid Id { get; init; }
}