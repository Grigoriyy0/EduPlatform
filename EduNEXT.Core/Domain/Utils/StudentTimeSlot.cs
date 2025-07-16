using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Core.Domain.Utils;

public class StudentTimeSlot
{
    public int Day { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Student? Student { get; set; }
}