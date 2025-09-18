using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ILessonsRepository
{
    Task CreateLessonAsync(Lesson lesson);
    
    Task DeleteLessonAsync(Lesson lesson);
    
    Task UpdateLessonAsync(Lesson lesson);
    
    Task<Lesson?> GetLessonAsync(Guid id);
    
    Task<bool> CheckAvailableLessonAsync(DateOnly date, TimeSpan start, TimeSpan end);
    
    Task<List<LessonDto>> GetLessonsAsync(string timePeriod);

    Task<List<LessonDto>> GetPendingLessonsAsync();
}