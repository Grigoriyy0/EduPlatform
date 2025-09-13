namespace EduNEXT.Application.Dtos.Analytics;

public class SalaryAnalyticsDto
{
    public decimal ActualSalary { get; set; }
    
    public decimal ExpectedSalary { get; set; }

    public List<decimal> DaysSalary { get; set; } = [];
}