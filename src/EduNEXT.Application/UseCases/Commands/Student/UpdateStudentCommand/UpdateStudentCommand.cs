using CSharpFunctionalExtensions;
using EduNEXT.Application.Dtos;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.UpdateStudentCommand;

public class UpdateStudentCommand : IRequest<UnitResult<Error>>
{
    public UpdateStudentDto dto { get; set; }
}