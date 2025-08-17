using MediatR;

namespace EduNEXT.Application.Events.TimeSlotCreated;

public class TimeSlotCreatedEvent : INotification
{
    public Guid StudentId { get; set; }
    
    public string StudentName { get; set; }
    
    public int Day { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
}