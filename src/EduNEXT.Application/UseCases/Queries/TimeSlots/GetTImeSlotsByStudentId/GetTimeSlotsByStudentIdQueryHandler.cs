using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.TimeSlots.GetTImeSlotsByStudentId;

public class GetTimeSlotsByStudentIdQueryHandler(ITimeSlotsRepository repository)
    : IRequestHandler<GetTimeSlotsByStudentIdQuery, List<StudentTimeSlot>>
{
    public async Task<List<StudentTimeSlot>> Handle(GetTimeSlotsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllByStudentIdAsync(request.StudentId);
    }
}