using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

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

    public Task UpdateLessonAsync(Lesson lesson)
    {
        context.Lessons.Update(lesson);
        
        return context.SaveChangesAsync();
    }

    public Task<Lesson?> GetLessonAsync(Guid id)
    {
        return context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckAvailableLessonAsync(DateOnly date, TimeSpan start, TimeSpan end)
    {
        var daysLessons = await context.Lessons.Where(x => x.Date == date)
            .ToListAsync();

        foreach (var dayLesson in daysLessons)
        {
            if (start < dayLesson.EndTime && dayLesson.StartTime < end)
            {
                return true;
            }
        }
        
        return false;
    }
}