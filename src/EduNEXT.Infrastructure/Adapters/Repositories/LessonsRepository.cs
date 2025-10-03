using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public sealed class LessonsRepository(MainContext context) : ILessonsRepository
{
    public async Task CreateLessonAsync(Lesson lesson)
    {
        await context.Lessons.AddAsync(lesson);
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
            if (start < dayLesson.EndTime && dayLesson.StartTime < end)
                return true;

        return false;
    }

    public Task<List<LessonDto>> GetLessonsAsync(string timePeriod)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        DateOnly startDate, endDate;

        switch (timePeriod.ToLower())
        {
            case "day":
                startDate = today;
                endDate = today;
                break;
            case "week":
                var diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
                startDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1 * diff).Date);
                endDate = startDate.AddDays(6);
                break;
            case "month":
                startDate = new DateOnly(today.Year, today.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
                break;
            default:
                startDate = DateOnly.MinValue;
                endDate = today;
                break;
        }

        return context.Lessons
            .AsNoTracking()
            .Where(l => l.Date >= startDate && l.Date <= endDate)
            .OrderBy(l => l.Date)
            .Select(l => new LessonDto
            {
                LessonId = l.Id,
                Date = l.Date,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                IsCompleted = l.IsCompleted,
                StudentName = l.Student!.Firstname + " " + l.Student.Lastname,
                LessonPrice = l.Student!.LessonPrice
            })
            .ToListAsync();
    }

    public async Task<List<LessonDto>> GetPendingLessonsAsync()
    {
        var today =  DateOnly.FromDateTime(DateTime.Now);
        
        return await context.Lessons
            .AsNoTracking()
            .Where(l => l.Date <= today && !l.IsCompleted)
            .OrderBy(l => l.Date)
            .Select(l => new LessonDto
            {
                LessonId = l.Id,
                Date = l.Date,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                IsCompleted = l.IsCompleted,
                StudentName = l.Student!.Firstname + " " + l.Student.Lastname,
                LessonPrice = l.Student!.LessonPrice
            })
            .ToListAsync();
    }

    public async Task<List<Lesson>> GetStudentLessonRangeAsync(DateOnly from, DateOnly to, Guid studentId)
    {
        return await context.Lessons
            .Where(l => l.Date >= from && l.Date <= to &&  l.StudentId == studentId)
            .ToListAsync();
    }

    public Task DeleteLessonsRangeAsync(List<Lesson> lessons)
    {
        context.Lessons.RemoveRange(lessons);
        return context.SaveChangesAsync();
    }
} 