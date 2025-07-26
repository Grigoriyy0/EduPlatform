using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Queries.TimeSlots.GetAllTimeSlots;

public class GetAllTimeSlotsQuery : IRequest<List<StudentTimeSlot>>
{
    
}