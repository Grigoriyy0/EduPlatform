using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.TimeSlot.UpdateTimeSlot;

public class UpdateTimeSlotCommand : IRequest<Result<StudentTimeSlot, Error>>
{
    public Guid TimeSlotId { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public int Day { get; set; }
}