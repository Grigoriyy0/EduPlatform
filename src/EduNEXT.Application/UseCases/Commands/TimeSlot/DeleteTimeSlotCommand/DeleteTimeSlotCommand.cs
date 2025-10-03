using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.TimeSlot.DeleteTimeSlotCommand;

public class DeleteTimeSlotCommand : IRequest<UnitResult<Error>>
{
    public Guid TimeSlotId { get; set; }
}