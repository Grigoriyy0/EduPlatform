using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class StudentTimeSlot
{
    public StudentTimeSlot()
    {
        
    }
    
    private StudentTimeSlot(
        Guid id, 
        int day, 
        TimeSpan start, 
        TimeSpan end, 
        TimeSpan duration, 
        Guid studentId)
    {
        Id = id;
        Day = day;
        StartTime = start;
        EndTime = end;
        StudentId = studentId;
        Duration = duration;
    }
    
    public Guid Id { get; set; }
    
    public int Day { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Student? Student { get; set; }

    public static Result<StudentTimeSlot, Error> Create(int day, TimeSpan startTime, TimeSpan endTime, Guid studentId)
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