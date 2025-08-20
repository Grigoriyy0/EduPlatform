using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Queries.TimeSlots.GetTImeSlotsByStudentId;

public class GetTimeSlotsByStudentIdQuery : IRequest<List<StudentTimeSlot>>
{
    public Guid StudentId { get; set; }
}