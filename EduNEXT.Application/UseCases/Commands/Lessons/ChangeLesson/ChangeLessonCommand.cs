using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.ChangeLesson;

public class ChangeLessonCommand : IRequest<Result<Lesson, Error>>
{
    public Guid LessonId { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
}