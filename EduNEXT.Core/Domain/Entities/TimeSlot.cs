namespace EduNEXT.Core.Domain.Entities;

public class TimeSlot
{
    public Guid Id { get; set; }
    
    public Guid? StudentId { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
}