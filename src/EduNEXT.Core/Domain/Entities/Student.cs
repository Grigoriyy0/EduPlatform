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

    public static Result<Student, Error> DecreasePaidLessonsCount(Student student, int amount)
    {
        if (student.PaidLessonsCount < amount)
        {
            student.PaidLessonsCount = 0;
            
            return student;
        }

        student.PaidLessonsCount -= amount;
        
        return student;
    }
    
}