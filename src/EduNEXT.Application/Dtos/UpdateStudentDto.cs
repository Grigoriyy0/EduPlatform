namespace EduNEXT.Application.Dtos;

public class UpdateStudentDto
{
    public Guid StudentId { get; set; }
    
    public string Name { get; set; }
    
    public string? Telegram { get; set; }
    
    public string TimeZone { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
}