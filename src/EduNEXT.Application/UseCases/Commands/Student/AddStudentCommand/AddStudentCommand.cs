using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentCommand;

public class AddStudentCommand : IRequest<Result<Core.Domain.Entities.Student, Error>>
{
    public string Name { get; set; }
    
    public string? Telegram { get; set; }
    
    public string? TimeZone { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
}