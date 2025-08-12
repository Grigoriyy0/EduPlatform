using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Repositories;

public sealed class LessonsRepository(MainContext context) : ILessonsRepository
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

    public Task<List<Lesson>> GetLessonsAsync(string timePeriod)
    {
        var todayDate = DateOnly.FromDateTime(DateTime.Now);
        
        var query = context.Lessons.AsQueryable();

        switch (timePeriod.ToLower())
        {
            case "day":
                var dayStart = todayDate;
                query = query.Where(l => l.Date >= dayStart);
                break;
            case "week":
                var weekStart = todayDate.AddDays(-(int)todayDate.DayOfWeek);
                query = query.Where(l => l.Date >= weekStart && l.Date <= todayDate);
                break;
            case "month":
                
                query = query.Where(l => l.Date.Month == todayDate.Month && l.Date.Year == todayDate.Year);
                break;
        }
        
        return query.OrderByDescending(x => x.Date).ToListAsync();
    }
}