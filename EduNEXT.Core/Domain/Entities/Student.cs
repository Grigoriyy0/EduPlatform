using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using EduNEXT.Core.Domain.ValueObjects;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class Student : Entity<Guid>
{
    public Student()
    {
        
    }
    
    private Student(string firstname, string lastname, EmailAddress email, string telegram, string password, int paidLessonsCount,
        int subscribedLessonsCount, decimal price)
    {
        Id = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Telegram = telegram;
        PasswordHash = password;
        PaidLessonsCount = paidLessonsCount;
        SubscribedLessonsCount = subscribedLessonsCount;
        LessonPrice = price;
        LessonTimeSlots = [];
    }
    
    public Guid Id { get; set; }
    
    public string Firstname { get; set; }
    
    public string Lastname { get; set; }
    
    public EmailAddress Email { get; set; }

    public string? Telegram { get; set; }
    
    public string PasswordHash { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
    
    public ICollection<StudentTimeSlot> LessonTimeSlots { get; set; }
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public static Result<Student, Error> Create(string firstname, string lastname, string emailAddress,
        string telegram, int paidLessonsCount, int subscribedLessonsCount, decimal lessonPrice, string password)
    {
        var email = EmailAddress.Create(emailAddress);
        
        if (email.IsFailure)
        {
            return Result.Failure<Student, Error>(email.Error);
        }
        
        if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname))
        {
            return DomainErrors.Student.NameIsIncorrect;
        }

        if (lessonPrice <= 0)
        {
            return DomainErrors.Student.LessonPriceIsIncorrect;
        }

        if (paidLessonsCount < 0)
        {
            return DomainErrors.Student.PaidLessonsCountIsIncorrect;
        }

        if (subscribedLessonsCount < 0)
        {
            return DomainErrors.Student.SubscribedLessonsCountIsIncorrect;
        }
        
        return new Student(firstname, lastname, email.Value, telegram, password, paidLessonsCount, subscribedLessonsCount, lessonPrice);
    }

    public static UnitResult<Error> AssignTimeSlot(Guid studentId, TimeOnly startTime, TimeOnly endTime, int day)
    {
        return StudentTimeSlot.Create(day, startTime, endTime, studentId);
    }
}