namespace EduNEXT.Application.Dtos.Analytics;

public class SalaryAnalyticsDto
{
    public double ActualSalary { get; set; }
    
    public double ExpectedSalary { get; set; }

    public List<double> DaysSalary { get; set; } = [];
}