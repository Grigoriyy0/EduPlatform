using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Application.UseCases.Events.TimeSlotDeleted;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.TimeSlot.DeleteTimeSlotCommand;

public class DeleteTimeSlotCommandHandler(ITimeSlotsRepository repository, IMediator mediator)
    : IRequestHandler<DeleteTimeSlotCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(DeleteTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var timeSlot = await repository.GetTimeSlotAsync(request.TimeSlotId);

        if (timeSlot == null)
        {
            return ApplicationErrors.TimeSlot.TimeSlotNotFound;
        }
        
        await repository.DeleteTimeSlotAsync(timeSlot);

        var notification = new TimeSlotDeletedEvent
        {
            StudentId = timeSlot.StudentId,
            StartTime = timeSlot.StartTime,
            EndTime = timeSlot.EndTime,
            Day = timeSlot.Day,
        };
        
        await mediator.Publish(notification, cancellationToken);
        
        return UnitResult.Success<Error>();
    }
}