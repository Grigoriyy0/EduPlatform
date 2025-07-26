using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.TimeSlot.AssignTimeSlotsCommand;

public class AssignTimeSlotsCommandHandler(ITimeSlotsRepository repository)
    : IRequestHandler<AssignTimeSlotsCommand, Result<StudentTimeSlot, Error>>
{
    public async Task<Result<StudentTimeSlot, Error>> Handle(AssignTimeSlotsCommand request, CancellationToken cancellationToken)
    {
        var checkAvailabilityFlag = await repository.CheckAvailabilityAsync(
            request.Day, 
            request.StartTime, 
            request.EndTime);

        if (checkAvailabilityFlag)
        {
            return ApplicationErrors.TimeSlot.TimeSlotIsBooked;
        }
        
        var timeSlot = StudentTimeSlot.Create(request.Day,
            request.StartTime,
            request.EndTime,
            request.StudentId);

        if (timeSlot.IsSuccess)
        {
            await repository.AddTimeSlotAsync(timeSlot.Value);
        }
        
        return timeSlot;
    }
}