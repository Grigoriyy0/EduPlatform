using Primitives;

namespace EduNEXT.Core.Domain.Errors;

public static class ApplicationErrors
{
    public static class TimeSlot
    {
        public static readonly Error TimeSlotIsBooked = GeneralErrors.ValueIsIncorrect(2, "timeslot.availability");
    }

    public static class Lesson
    {
        public static readonly Error LessonIsNotExists = GeneralErrors.ValueIsIncorrect(2, "lesson.notExists");
        
        public static readonly Error LessonTimeIsBooked = GeneralErrors.ValueIsIncorrect(2, "lesson.times.booked");
    }
}