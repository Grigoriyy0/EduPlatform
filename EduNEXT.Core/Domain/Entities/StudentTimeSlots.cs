namespace EduNEXT.Core.Domain.Entities;

public class StudentTimeSlots
{
    public Guid Id { get; set; }
    
    public int[] Days { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Student Student { get; set; }
}