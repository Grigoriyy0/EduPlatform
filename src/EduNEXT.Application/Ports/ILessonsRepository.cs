using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ILessonsRepository
{
    Task AddAsync(Lesson lesson);
    
    Task DeleteAsync(Lesson lesson);
    
    Task UpdateAsync(Lesson lesson);
    
    Task<Lesson?> GetByIdAsync(Guid id);
    
    Task<List<LessonDto>> GetByPeriodAsync(string timePeriod);

    Task<List<LessonDto>> GetPendingAsync();

    Task<List<Lesson>> GetStudentLessonRangeAsync(DateOnly from, DateOnly to, Guid studentId);
    
    Task<List<Guid>> GetInterferedLessonsAsync(DateOnly date, TimeSpan start, TimeSpan end);
}