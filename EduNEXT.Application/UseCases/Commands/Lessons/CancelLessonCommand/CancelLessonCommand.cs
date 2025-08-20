using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.CancelLessonCommand;

public class CancelLessonCommand : IRequest<UnitResult<Error>>
{
    public Guid LessonId { get; set; }
}