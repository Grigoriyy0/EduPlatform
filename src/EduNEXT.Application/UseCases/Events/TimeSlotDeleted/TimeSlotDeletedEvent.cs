using MediatR;

namespace EduNEXT.Application.UseCases.Events.TimeSlotDeleted;

public class TimeSlotDeletedEvent : INotification
{
    public Guid StudentId { get; set; }
    
    public int Day { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
}