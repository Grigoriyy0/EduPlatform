using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;

namespace EduNEXT.Infrastructure.Repositories;

public class LessonsRepository(MainContext context) : ILessonsRepository
{
    public async Task CreateLessonAsync(Lesson lesson)
    {
        await  context.Lessons.AddAsync(lesson);
        await context.SaveChangesAsync();
    }

    public async Task DeleteLessonAsync(Lesson lesson)
    {
        context.Lessons.Remove(lesson);
        
        await context.SaveChangesAsync();
    }
}