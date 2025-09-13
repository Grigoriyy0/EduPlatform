using EduNEXT.Application.Dtos;
using EduNEXT.Application.Dtos.Analytics;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Lessons.GetAnalyticsData;

public class GetLessonsAnalyticsHandler(ILessonsRepository repository)
    : IRequestHandler<GetLessonsAnalyticsQuery, LessonsAnalyticsDto>
{
    public async Task<LessonsAnalyticsDto> Handle(GetLessonsAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var lessonsToday = await repository.GetLessonsAsync("day");
        
        var lessonsMonth = await repository.GetLessonsAsync("month");

        var pendingLessons = await repository.GetPendingLessonsAsync();
        
        var lessonsWeek = await repository.GetLessonsAsync("week");
        
        var diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
        
        var weekStart = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1 * diff));
        
        var weekEnd = weekStart.AddDays(6);
        
        var lessonsDay = CalculateLessonDays(lessonsWeek, weekStart, weekEnd);
        
        var lessonAnalyticsDto = new LessonsAnalyticsDto
        {
            LessonsCount = lessonsMonth.Count,
            LessonsTodayCount = lessonsToday.Count,
            LessonsPending = pendingLessons,
            LessonsDayCount = lessonsDay
        };
        
        return lessonAnalyticsDto;
    }
    
    private List<int> CalculateLessonDays(List<LessonDto> lessonsList, DateOnly startDate, DateOnly endDate)
    {
        var dailySalaries = new Dictionary<DateOnly, int>();
        
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            dailySalaries[date] = 0;
        }

        foreach (var lesson in lessonsList)
        {
            var lessonDate = lesson.Date;
            if (dailySalaries.ContainsKey(lessonDate))
            {
                dailySalaries[lessonDate] += 1;
            }
        }
        
        return dailySalaries
            .OrderBy(x => x.Key)
            .Select(x => x.Value)
            .ToList();
    }
}