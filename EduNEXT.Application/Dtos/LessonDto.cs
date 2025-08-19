namespace EduNEXT.Application.Dtos;

public class LessonDto
{
    public Guid LessonId { get; set; }
    
    public DateOnly Date { get; set; } 
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public string StudentName { get; set; }
}