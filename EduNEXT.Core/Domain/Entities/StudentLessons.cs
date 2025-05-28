namespace EduNEXT.Core.Domain.Entities;

public class StudentLessons
{
    public Guid LessonId { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Lesson? Lesson { get; set; }
    
    public Student? Student { get; set; }
}