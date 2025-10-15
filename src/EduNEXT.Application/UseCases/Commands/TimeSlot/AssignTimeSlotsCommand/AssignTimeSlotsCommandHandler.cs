using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Application.UseCases.Events.TimeSlotCreated;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.TimeSlot.AssignTimeSlotsCommand;

public class AssignTimeSlotsCommandHandler(
    ITimeSlotsRepository repository, 
    IMediator mediator,
    IStudentRepository studentRepository)
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

            var student = await studentRepository.FindByIdAsync(request.StudentId);
            
            await mediator.Publish(new TimeSlotCreatedEvent
                {
                    Day = request.Day != 7 ? request.Day : 0,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    StudentId = request.StudentId,
                    StudentName = student!.Name
                },
                cancellationToken);
        }
        
        return timeSlot;
    }
}