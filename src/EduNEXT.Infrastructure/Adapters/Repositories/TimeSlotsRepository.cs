using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public sealed class TimeSlotsRepository(MainContext context) : ITimeSlotsRepository
{
    public async Task AddTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct)
    {
        await context.LessonsTimeSlots.AddAsync(timeSlot, ct);
        await context.SaveChangesAsync(ct);
    }

    public Task UpdateTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct)
    {
        context.LessonsTimeSlots.Update(timeSlot);
        return context.SaveChangesAsync(ct);
    }

    public Task DeleteTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct)
    {
        context.LessonsTimeSlots.Remove(timeSlot);
        return context.SaveChangesAsync(ct);
    }

    public async Task<List<StudentTimeSlot>> GetAllByStudentIdAsync(Guid studentId, CancellationToken ct)
    {
        return await context.LessonsTimeSlots.Where(x => x.StudentId == studentId)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<bool> CheckAvailabilityAsync(int day, TimeSpan startTime, TimeSpan endTime, CancellationToken ct)
    {
        var timeSlotDay = await context.LessonsTimeSlots.Where(x => x.Day == day)
            .ToListAsync(ct);

        foreach (var timeSlot in timeSlotDay)
        {
            if (startTime < timeSlot.EndTime && timeSlot.StartTime < endTime)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<List<TimeSlotDto>> GetAllTimeSlotsAsync(CancellationToken ct)
    {
        return await context.LessonsTimeSlots
            .Include(x => x.Student)
            .Select(ts => new TimeSlotDto
            {
                TimeSlotId = ts.Id,
                Day = ts.Day,
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                StudentName = ts.Student.Name,
            }).ToListAsync(ct);
    }

    public Task<StudentTimeSlot?> GetTimeSlotAsync(Guid timeSlotId, CancellationToken ct)
    {
        return context.LessonsTimeSlots.FirstOrDefaultAsync(x => x.Id == timeSlotId, ct);
    }
    
}
    