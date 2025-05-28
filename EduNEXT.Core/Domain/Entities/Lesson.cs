namespace EduNEXT.Core.Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public decimal Price { get; set; }
    
    public List<StudentLessons> StudentLessons { get; set; }
}