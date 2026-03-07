using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public sealed class LessonsRepository(MainContext context) : ILessonsRepository
{
    public Task AddAsync(Lesson lesson, CancellationToken ct)
    {
        context.Lessons.AddAsync(lesson, ct).AsTask();
        return context.SaveChangesAsync(ct);
    }

    public Task DeleteAsync(Lesson lesson, CancellationToken ct)
    {
        context.Lessons.Remove(lesson);

        return context.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(Lesson lesson, CancellationToken ct)
    {
        context.Lessons.Update(lesson);

        return context.SaveChangesAsync(ct);
    }

    public Task<Lesson?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return context.Lessons.FirstOrDefaultAsync(x => x.Id == id, ct);
    }
    
    public async Task<List<Guid>> GetInterferedLessonsAsync(DateOnly date, TimeSpan start, TimeSpan end, CancellationToken  ct)
    {
        var lessonGuids = new List<Guid>();
        
        var daysLessons = await context.Lessons.Where(x => x.Date == date)
            .ToListAsync(ct);

        foreach (var dayLesson in daysLessons)
        {
            if (start < dayLesson.EndTime && dayLesson.StartTime < end)
            {
                lessonGuids.Add(dayLesson.Id);
            }
        }
        
        return lessonGuids;
    }

    public Task<List<LessonDto>> GetByPeriodAsync(string timePeriod, CancellationToken ct)
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
                StudentName = l.Student!.Name,
                LessonPrice = l.Student!.LessonPrice
            })
            .ToListAsync(ct);
    }

    public Task<List<LessonDto>> GetPendingAsync(CancellationToken ct)
    {
        var today =  DateOnly.FromDateTime(DateTime.Now);
        
        return context.Lessons
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
                StudentName = l.Student!.Name,
                LessonPrice = l.Student!.LessonPrice
            })
            .ToListAsync(ct);
    }

    public Task<List<Lesson>> GetStudentLessonRangeAsync(DateOnly from, DateOnly to, Guid studentId, CancellationToken ct)
    {
        return context.Lessons
            .Where(l => l.Date >= from && l.Date <= to &&  l.StudentId == studentId)
            .ToListAsync(ct);
    }
} 