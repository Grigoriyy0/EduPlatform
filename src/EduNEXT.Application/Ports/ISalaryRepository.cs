namespace EduNEXT.Application.Ports;

public interface ISalaryRepository
{
    public Task<decimal> GetExpectedSalary();
    
    public Task<decimal> GetActualSalary();

    public Task<decimal> GetSalaryToday();
}