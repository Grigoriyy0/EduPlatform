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
    
    private Student(string name, string telegram, string password, int paidLessonsCount,
        int subscribedLessonsCount, decimal price, string? timezone = "HKT")
    {
        Name = name;
        Telegram = telegram;
        TimeZone = timezone;
        PasswordHash = password;
        PaidLessonsCount = paidLessonsCount;
        SubscribedLessonsCount = subscribedLessonsCount;
        LessonPrice = price;
        LessonTimeSlots = [];
    }
    
    public string Name { get; set; }
    
    public string? Telegram { get; set; }
    
    public string? TimeZone { get; set; }
    
    public string PasswordHash { get; set; }
    
    public int PaidLessonsCount { get; set; }
    
    public int SubscribedLessonsCount { get; set; }
    
    public decimal LessonPrice { get; set; }
    
    public ICollection<StudentTimeSlot> LessonTimeSlots { get; set; }
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public static Result<Student, Error> Create(string name, string? timezone,
        string telegram, int paidLessonsCount, int subscribedLessonsCount, decimal lessonPrice, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
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
        
        return new Student(name, telegram, password, paidLessonsCount, subscribedLessonsCount, lessonPrice, timezone);
    }

    public UnitResult<Error> DecreasePaidLessonsCount()
    {
        if (PaidLessonsCount == 0)
        {
            return DomainErrors.Student.DecreaseZeroPaidLessons;
        }
        
        PaidLessonsCount--;
        
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddPaidLessons(int amount)
    {
        if (amount <= 0)
        {
            return DomainErrors.Student.PaidLessonsCountIsIncorrect;
        }
        
        PaidLessonsCount +=  amount;
        
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> Update(
        string name, 
        int paidLessonsCount, 
        int subscribedLessonsCount, 
        string telegram, 
        string timezone, 
        decimal lessonPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return DomainErrors.Student.NameIsIncorrect;
        }

        if (paidLessonsCount < 0)
        {
            return DomainErrors.Student.PaidLessonsCountIsIncorrect;
        }

        if (subscribedLessonsCount < 0)
        {
            return DomainErrors.Student.SubscribedLessonsCountIsIncorrect;
        }

        if (lessonPrice <= 0)
        {
            return DomainErrors.Student.LessonPriceIsIncorrect;
        }

        Name = name;
        PaidLessonsCount = paidLessonsCount;
        SubscribedLessonsCount = subscribedLessonsCount;
        Telegram = telegram;
        TimeZone = timezone;
        LessonPrice = lessonPrice;
        
        return UnitResult.Success<Error>();
    }
}