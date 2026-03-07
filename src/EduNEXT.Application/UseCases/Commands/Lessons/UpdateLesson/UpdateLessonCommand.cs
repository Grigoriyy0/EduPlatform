using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.UpdateLesson;

public class UpdateLessonCommand : IRequest<UnitResult<Error>>
{
    public Guid LessonId { get; set; }
    
    public DateOnly Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public bool IsCompleted { get; set; }
}