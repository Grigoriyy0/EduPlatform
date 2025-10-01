using MediatR;

namespace EduNEXT.Application.UseCases.Events.TimeSlotDeleted;

public class TimeSlotDeletedEventHandler : INotificationHandler<TimeSlotDeletedEvent>
{
    public Task Handle(TimeSlotDeletedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}