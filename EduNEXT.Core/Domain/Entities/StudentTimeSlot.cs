using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class StudentTimeSlot
{
    private StudentTimeSlot(
        Guid id, 
        int day, 
        TimeOnly start, 
        TimeOnly end, 
        TimeSpan duration, 
        Guid studentId)
    {
        Id = id;
        Days = day;
        StartTime = start;
        EndTime = end;
        StudentId = studentId;
        Duration = duration;
    }
    
    public Guid Id { get; set; }
    
    public int Days { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Student? Student { get; set; }

    public static Result<StudentTimeSlot, Error> Create(int day, TimeOnly startTime, TimeOnly endTime, Guid studentId)
    {
        if (startTime > endTime)
        {
            return Result.Failure<StudentTimeSlot, Error>(DomainErrors.TimeSlot.EndIsEarlier);
        }

        if (day < 0 || day > 7)
        {
            return Result.Failure<StudentTimeSlot, Error>(DomainErrors.TimeSlot.DayIsIncorrect);
        }
        
        var duration = endTime - startTime;
        
        return new StudentTimeSlot(Guid.NewGuid(), day, startTime, endTime, duration, studentId);
    } 
}