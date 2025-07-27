using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Infrastructure.Adapters;

public class LessonsScheduler
{
    private readonly ILessonsRepository _lessonsRepository;

    public LessonsScheduler(ILessonsRepository lessonsRepository)
    {
        _lessonsRepository = lessonsRepository;
    }

    public async Task PlanLessonsForNewStudent(int day, TimeOnly startTime, TimeOnly endTime, Guid studentId)
    {
    }
}