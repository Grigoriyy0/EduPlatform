using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.TimeSlot.AssignTimeSlotsCommand;

public class AssignTimeSlotsCommand : IRequest<Result<StudentTimeSlot, Error>>
{
    public Guid StudentId { get; set; }
    
    public int Day { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}