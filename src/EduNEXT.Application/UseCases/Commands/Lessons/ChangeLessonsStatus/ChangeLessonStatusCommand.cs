using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommand : IRequest<UnitResult<Error>>
{
    public Guid LessonId { get; set; }
}