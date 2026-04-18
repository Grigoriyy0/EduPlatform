using System.Text.RegularExpressions;
using Primitives;

namespace EduNEXT.Core.Domain.Errors;

public static class DomainErrors
{
    public static class Student
    {
        public static readonly Error EmailIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.email");
        
        public static readonly Error NameIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.name");
        
        public static readonly Error LessonPriceIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.lesson.price");
        
        public static readonly Error PaidLessonsCountIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.paid.count");
        
        public static readonly Error SubscribedLessonsCountIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.subscribed.count");
        
        public static readonly Error DecreaseZeroPaidLessons = GeneralErrors.ValueIsIncorrect(1, "user.decrease.count");
    }

    public static class TimeSlot
    {
        public static readonly Error EndIsEarlier = GeneralErrors.ValueIsIncorrect(1, "timeslot.end");

        public static readonly Error DayIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "day.number");
        
    }

    public static class Lesson
    {
        public static readonly Error EndTimeIsEarlier = GeneralErrors.ValueIsIncorrect(1, "lesson.end");

        public static readonly Error CompleteCompleted = GeneralErrors.ValueIsIncorrect(1, "lesson.complete");
    }
}