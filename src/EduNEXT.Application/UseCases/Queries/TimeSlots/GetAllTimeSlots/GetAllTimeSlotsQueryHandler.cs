using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.TimeSlots.GetAllTimeSlots;

public class GetAllTimeSlotsQueryHandler(ITimeSlotsRepository repository)
    : IRequestHandler<GetAllTimeSlotsQuery, List<StudentTimeSlot>>
{
    public async Task<List<StudentTimeSlot>> Handle(GetAllTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllTimeSlotsAsync();
    }   
}