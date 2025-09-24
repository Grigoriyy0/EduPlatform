namespace EduNEXT.Application.Dtos;

public class TimeSlotDto
{
    public int Day { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
}