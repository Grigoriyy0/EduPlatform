using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ITimeSlotsRepository
{
    Task AddTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task UpdateTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task DeleteTimeSlotAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task<List<StudentTimeSlot>> GetAllByStudentIdAsync(Guid studentId, CancellationToken ct);
    
    Task<bool> CheckAvailabilityAsync(int day, TimeSpan startTime, TimeSpan endTime, CancellationToken ct);
    
    Task<List<TimeSlotDto>> GetAllTimeSlotsAsync(CancellationToken ct);
    
    Task<StudentTimeSlot?> GetTimeSlotAsync(Guid timeSlotId, CancellationToken ct);
    
}