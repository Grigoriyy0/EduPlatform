using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ILessonsRepository
{
    Task CreateLessonAsync(Lesson lesson);
    
    Task DeleteLessonAsync(Lesson lesson);
}