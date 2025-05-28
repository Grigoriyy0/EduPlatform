namespace EduNEXT.Core.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }

    public string? AlternativeEmail { get; set; }
    
    public string PasswordHash { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public List<StudentLessons> StudentLessons { get; set; }
}