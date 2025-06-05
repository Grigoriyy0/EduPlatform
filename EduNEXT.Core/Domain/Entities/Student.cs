using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Errors;
using EduNEXT.Core.Domain.ValueObjects;
using Primitives;

namespace EduNEXT.Core.Domain.Entities;

public class Student : Entity<Guid>
{
    private Student(string firstname, string lastname, EmailAddress email, string telegram, string password, int paidLessonsCount,
        decimal price)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Telegram = telegram;
        PasswordHash = password;
        PaidLessonsCount = paidLessonsCount;
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
    
    public ICollection<StudentTimeSlots> LessonTimeSlots { get; set; }

    public static Result<Student, Error> Create(string firstname, string lastname, string emailAddress,
        string telegram, int paidLessonsCount, decimal lessonPrice)
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

        if (lessonPrice < 0)
        {
            return DomainErrors.Student.LessonPriceIsIncorrect;
        }

        if (paidLessonsCount < 0)
        {
            return DomainErrors.Student.PaidLessonsCountIsIncorrect;
        }
        
        return new Student(firstname, lastname, email.Value, telegram, "fsfef" ,paidLessonsCount, lessonPrice);
    }

    public static UnitResult<Error> AssignTimeSlots(Student student)
    {
        return default;
    }
}