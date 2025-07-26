using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Student.AddStudentCommand;

public class AddStudentCommand : IRequest<Result<Core.Domain.Entities.Student, Error>>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string? Telegram { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
}