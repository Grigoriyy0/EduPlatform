using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class Lesson : Entity<Guid>
{
    public DateOnly Date { get; private set; }
    
    public Guid StudentId { get; private set; }
    
    public TimeSpan StartTime { get; private set; }
    
    public TimeSpan EndTime { get; private set; }
    
    public bool IsCompleted { get; private set; }
    
    public Student? Student { get; private set; }

    private Lesson(DateOnly date, TimeSpan startTime, TimeSpan endTime, Guid studentId)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        StudentId = studentId;
        IsCompleted = false;
    }

    public static Result<Lesson, Error> Create(DateOnly date, TimeSpan start, TimeSpan end, Guid studentId)
    {
        if (start >= end)
            return DomainErrors.Lesson.EndTimeIsEarlier;

        return new Lesson(date, start, end, studentId);
    }

    public UnitResult<Error> Complete()
    {
        if (IsCompleted)
        {
            return DomainErrors.Lesson.CompleteCompleted;
        }
        
        IsCompleted = true;
        
        return UnitResult.Success<Error>();
    }
    
    public UnitResult<Error> Update(DateOnly date, TimeSpan startTime, TimeSpan endTime, bool isCompleted)
    {
        if (startTime < endTime)
        {
            return DomainErrors.Lesson.EndTimeIsEarlier;
        }
        
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        IsCompleted = isCompleted;

        return UnitResult.Success<Error>();
    }
}