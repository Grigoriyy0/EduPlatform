using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Utils;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.AddStudentCommand;

public class AddStudentCommand : IRequest<Result<Student, Error>>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string? Telegram { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
}