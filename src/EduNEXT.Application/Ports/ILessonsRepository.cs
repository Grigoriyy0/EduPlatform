using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ILessonsRepository
{
    Task AddAsync(Lesson lesson, CancellationToken ct);
    
    Task DeleteAsync(Lesson lesson, CancellationToken ct);
    
    Task UpdateAsync(Lesson lesson, CancellationToken ct);
    
    Task<Lesson?> GetByIdAsync(Guid id, CancellationToken ct);
    
    Task<List<LessonDto>> GetByPeriodAsync(string timePeriod, CancellationToken ct);

    Task<List<LessonDto>> GetPendingAsync(CancellationToken ct);

    Task<List<Lesson>> GetStudentLessonRangeAsync(DateOnly from, DateOnly to, Guid studentId, CancellationToken ct);
    
    Task<List<Guid>> GetInterferedLessonsAsync(DateOnly date, TimeSpan start, TimeSpan end, CancellationToken ct);
}