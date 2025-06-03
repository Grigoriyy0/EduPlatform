using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Utils;

namespace EduNEXT.Application.Services;

public class TimeTableService
{
    public async Task<TimeSlot[]> CreateTimeSlots(TimeSlotRange[] timeSlotRanges)
    {
        var slots = new TimeSlot[timeSlotRanges.Length];
        
        var firstSlot = TimeSlot.Create(Guid.NewGuid(), timeSlotRanges[0].Start, timeSlotRanges[0].End);
        if (firstSlot.IsFailure)
            throw new Exception($"Не удалось создать первый TimeSlot: {firstSlot.Error}.");
        slots[0] = firstSlot.Value;
        
        for (var i = 1; i < timeSlotRanges.Length; i++)
        {
            if (timeSlotRanges[i].Start < timeSlotRanges[i - 1].End)
                throw new Exception($"Слот {i} начинается в {timeSlotRanges[i].Start}, но предыдущий слот заканчивается только в {timeSlotRanges[i - 1].End}!");
            
            var currentSlot = TimeSlot.Create(Guid.NewGuid(), timeSlotRanges[i].Start, timeSlotRanges[i].End);
            if (currentSlot.IsFailure)
                throw new Exception($"Не удалось создать слот {i}: {currentSlot.Error}.");

            slots[i] = currentSlot.Value;
        }

        return slots;
    }
}