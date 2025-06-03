using EduNEXT.Core.Domain.ValueObjects;

namespace EduNEXT.Core.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public EmailAddress Email { get; set; }

    public string? AlternativeEmail { get; set; }
    
    public string PasswordHash { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal PaidLessonsPrice { get; set; }
    
    public ICollection<StudentTimeSlots> LessonTimeSlots { get; set; }
}