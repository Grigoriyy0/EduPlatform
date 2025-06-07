namespace EduNEXT.Core.Domain.Entities;

public class StudentTimeSlots
{
    private StudentTimeSlots(
        Guid id, 
        int[] days, 
        TimeOnly start, 
        TimeOnly end, 
        TimeSpan duration, 
        Guid studentId)
    {
        Id = id;
        Days = days;
        StartTime = start;
        EndTime = end;
    }
    
    public Guid Id { get; set; }
    
    public int[] Days { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public bool IsCompleted { get; set; } = true;
    
    public Guid StudentId { get; set; }
    
    public Student Student { get; set; }
    
}