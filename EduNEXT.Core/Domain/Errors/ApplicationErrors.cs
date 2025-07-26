using Primitives;

namespace EduNEXT.Core.Domain.Errors;

public static class ApplicationErrors
{
    public static class TimeSlot
    {
        public static readonly Error TimeSlotIsBooked = GeneralErrors.ValueIsIncorrect(2, "timeslot.availability");
    }
}