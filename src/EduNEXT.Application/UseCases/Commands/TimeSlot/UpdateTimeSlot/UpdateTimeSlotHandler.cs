using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.TimeSlot.UpdateTimeSlot;

public class UpdateTimeSlotHandler(ITimeSlotsRepository repository)
    : IRequestHandler<UpdateTimeSlotCommand, Result<StudentTimeSlot, Error>>
{
    public async Task<Result<StudentTimeSlot, Error>> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var timeSlot = await repository.GetTimeSlotAsync(request.TimeSlotId);

        if (request.StartTime > request.EndTime)
        {
            return DomainErrors.TimeSlot.EndIsEarlier;
        }
        
        if (request.Day < 1 || request.Day > 7)
        {
            return DomainErrors.TimeSlot.DayIsIncorrect;
        }
        
        if (timeSlot is null)
        {
            return ApplicationErrors.TimeSlot.TimeSlotNotFound;
        }
        
        var available = await repository.CheckAvailabilityAsync(request.Day, request.StartTime, request.EndTime);

        if (available)
        {
            return ApplicationErrors.TimeSlot.TimeSlotIsBooked;
        }

        timeSlot.Day = request.Day;
        timeSlot.StartTime = request.StartTime;
        timeSlot.EndTime = request.EndTime;
        
        await repository.UpdateTimeSlotAsync(timeSlot);

        return timeSlot;
    }
}