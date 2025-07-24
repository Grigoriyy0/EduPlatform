using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class Lesson
{
    private Lesson(DateTime date, DateTime startTime, DateTime endTime, Guid studentId)
    {
        Id = Guid.NewGuid();
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        StudentId = studentId;
    }
    
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public Guid StudentId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public Student? Student { get; set; }

    public static Result<Lesson, Error> Create(DateTime date, DateTime startTime, DateTime endTime, Guid studentId)
    {
        if (startTime > endTime)
        {
            return DomainErrors.Lesson.EndTimeIsEarlier;
        }
        
        return new Lesson(date, startTime, endTime, studentId);
    }
}