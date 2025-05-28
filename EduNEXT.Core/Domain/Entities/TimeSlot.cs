using CSharpFunctionalExtensions;

namespace EduNEXT.Core.Domain.Entities;

public class TimeSlot
{
    private TimeSlot(
        Guid id,
        Guid? studentId,
        bool isAvailable,
        DateTime startTime,
        DateTime endTime)
    {
        Id = id;
        StudentId = studentId;
        IsAvailable = isAvailable;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public Guid Id { get; set; }
    
    public Guid? StudentId { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    public static Result<TimeSlot, string> Create(
        Guid id,
        DateTime startTime,
        DateTime endTime)
    {
        if (startTime > endTime)
        {
            return Result.Failure<TimeSlot, string>("Start time can't be after End time");
        }
        
        return new TimeSlot(id, null, true, startTime, endTime);
    }
}