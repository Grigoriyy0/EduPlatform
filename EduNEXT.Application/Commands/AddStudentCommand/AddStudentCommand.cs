using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Utils;
using MediatR;

namespace EduNEXT.Application.Commands.AddStudentCommand;

public class AddStudentCommand : IRequest<Student>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public StudentTimeSlot[] TimeSlots {get; set;}
    
    public string Email { get; set; }
    
    public string? AlternativeEmail { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
}