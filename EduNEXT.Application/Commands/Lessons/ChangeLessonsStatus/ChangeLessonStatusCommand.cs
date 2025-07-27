using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommand : IRequest<Result<Lesson, Error>>
{
    public Guid LessonId { get; set; }
}