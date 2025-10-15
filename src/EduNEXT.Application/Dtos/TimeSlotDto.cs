namespace EduNEXT.Application.Dtos;

public class TimeSlotDto
{
    public Guid TimeSlotId { get; set; }
    
    public int Day { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public string StudentName { get; set; }
}