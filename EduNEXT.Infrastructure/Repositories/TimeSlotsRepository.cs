using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Repositories;

public class TimeSlotsRepository(MainContext context) : ITimeSlotsRepository
{
    public async Task AddTimeSlotAsync(StudentTimeSlot timeSlot)
    {
        await context.LessonsTimeSlots.AddAsync(timeSlot);
        await context.SaveChangesAsync();
    }

    public Task UpdateTimeSlotAsync(StudentTimeSlot timeSlot)
    {
        context.LessonsTimeSlots.Update(timeSlot);
        return context.SaveChangesAsync();
    }

    public Task DeleteTimeSlotAsync(StudentTimeSlot timeSlot)
    {
        context.LessonsTimeSlots.Remove(timeSlot);
        return context.SaveChangesAsync();
    }

    public async Task<List<StudentTimeSlot>> GetAllByStudentIdAsync(Guid studentId)
    {
        return await context.LessonsTimeSlots.Where(x => x.StudentId == studentId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> CheckAvailabilityAsync(int day, TimeOnly startTime, TimeOnly endTime)
    {
        var timeSlotDay = await context.LessonsTimeSlots.Where(x => x.Day == day)
            .ToListAsync();

        foreach (var timeSlot in timeSlotDay)
        {
            if (startTime < timeSlot.EndTime && timeSlot.StartTime < endTime)
            {
                return true;
            }
        }

        return false;
    }

    public Task<List<StudentTimeSlot>> GetAllTimeSlotsAsync()
    {
        return context.LessonsTimeSlots.AsNoTracking().ToListAsync();
    }
}
    