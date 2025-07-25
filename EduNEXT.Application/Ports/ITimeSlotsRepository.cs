using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ITimeSlotsRepository
{
    Task AddTimeSlotAsync(StudentTimeSlot timeSlot);
    
    Task UpdateTimeSlotAsync(StudentTimeSlot timeSlot);
    
    Task DeleteTimeSlotAsync(StudentTimeSlot timeSlot);
    
    Task<List<StudentTimeSlot>> GetAllByStudentIdAsync(Guid studentId);
    
    Task<bool> CheckAvailabilityAsync(int day, TimeOnly startTime, TimeOnly endTime);
}