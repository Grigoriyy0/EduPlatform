namespace EduNEXT.Application.Dtos;

public class StudentDto
{
    public Guid StudentId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Telegram { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
    
    public List<TimeSlotDto> TimeSlots { get; set; }
}