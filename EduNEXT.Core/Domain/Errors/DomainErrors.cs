using Primitives;

namespace EduNEXT.Core.Domain.Errors;

public static class DomainErrors
{
    public static class Student
    {
        public static Error EmailIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.email");
        
        public static Error NameIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.name");
        
        public static Error LessonPriceIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.lesson.price");
        
        public static Error PaidLessonsCountIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.paid.count");
        
        public static Error SubscribedLessonsCountIsIncorrect = GeneralErrors.ValueIsIncorrect(1, "user.subscribed.count");
    }
}