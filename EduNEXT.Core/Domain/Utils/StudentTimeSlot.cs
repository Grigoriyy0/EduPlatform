namespace EduNEXT.Core.Domain.Utils;

public class StudentTimeSlot
{
    public int Day { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}