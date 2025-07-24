using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.AssignTimeSlotsCommand;

public class AssignTimeSlotsCommandHandler : IRequestHandler<AssignTimeSlotsCommand, Result<StudentTimeSlot, Error>>
{
    public async Task<Result<StudentTimeSlot, Error>> Handle(AssignTimeSlotsCommand request, CancellationToken cancellationToken)
    {
        var timeSlot = StudentTimeSlot.Create(request.Day,
            request.StartTime,
            request.EndTime,
            request.StudentId);


        return timeSlot;
    }
}