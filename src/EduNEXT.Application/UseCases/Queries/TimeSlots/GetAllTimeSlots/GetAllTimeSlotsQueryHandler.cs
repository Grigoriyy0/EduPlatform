using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.TimeSlots.GetAllTimeSlots;

public class GetAllTimeSlotsQueryHandler(ITimeSlotsRepository repository)
    : IRequestHandler<GetAllTimeSlotsQuery, List<TimeSlotDto>>
{
    public async Task<List<TimeSlotDto>> Handle(GetAllTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllTimeSlotsAsync();
    }   
}