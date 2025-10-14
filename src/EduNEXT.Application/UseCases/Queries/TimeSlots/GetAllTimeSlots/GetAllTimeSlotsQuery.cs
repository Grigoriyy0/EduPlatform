using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.TimeSlots.GetAllTimeSlots;

public class GetAllTimeSlotsQuery : IRequest<List<TimeSlotDto>>
{
    
}