using EduNEXT.Application.Dtos;
using EduNEXT.Application.Dtos.Analytics;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Salary.GetDataForAnalytics;

public class GetSalaryAnalyticsHandler(ISalaryRepository salaryRepository, ILessonsRepository lessonsRepository) : IRequestHandler<GetSalaryAnalyticsQuery, SalaryAnalyticsDto>
{
    public async Task<SalaryAnalyticsDto> Handle(GetSalaryAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var actualSalary = await salaryRepository.GetActualSalary();
        
        var expectedSalary = await salaryRepository.GetExpectedSalary();

        var lessonsWeek = await lessonsRepository.GetLessonsAsync("week");
        
        var diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
        
        var weekStart = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1 * diff));
        
        var weekEnd = weekStart.AddDays(6);
        
        var daysSalary = CalculateDaysSalary(lessonsWeek, weekStart, weekEnd);
        
        return new SalaryAnalyticsDto
        {
            ActualSalary = actualSalary,
            ExpectedSalary = expectedSalary,
            DaysSalary = daysSalary
        };
    }


    private List<decimal> CalculateDaysSalary(List<LessonDto> lessonsList, DateOnly startDate, DateOnly endDate)
    {
        var dailySalaries = new Dictionary<DateOnly, decimal>();
        
        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            dailySalaries[date] = 0;
        }

        foreach (var lesson in lessonsList)
        {
            var lessonDate = lesson.Date;
            if (dailySalaries.ContainsKey(lessonDate))
            {
                dailySalaries[lessonDate] += lesson.LessonPrice;
            }
        }
        
        return dailySalaries
            .OrderBy(x => x.Key)
            .Select(x => x.Value)
            .ToList();
    }
}