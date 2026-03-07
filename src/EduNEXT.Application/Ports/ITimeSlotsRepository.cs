using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ITimeSlotsRepository
{
    Task AddAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task UpdateAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task DeleteAsync(StudentTimeSlot timeSlot, CancellationToken ct);
    
    Task<List<StudentTimeSlot>> GetByStudentIdAsync(Guid studentId, CancellationToken ct);
    
    Task<bool> CheckAvailabilityAsync(int day, TimeSpan startTime, TimeSpan endTime, CancellationToken ct);
    
    Task<List<TimeSlotDto>> GetDtoAsync(CancellationToken ct);
    
    Task<StudentTimeSlot?> GetAsync(Guid timeSlotId, CancellationToken ct);
    
}