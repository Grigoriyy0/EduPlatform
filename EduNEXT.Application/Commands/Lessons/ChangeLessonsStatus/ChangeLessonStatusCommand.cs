using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommand : IRequest<Result<Unit, Error>>
{
    public Guid LessonId { get; set; }
}