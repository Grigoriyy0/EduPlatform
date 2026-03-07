namespace EduNEXT.Application.Ports;

public interface ISalaryRepository
{
    public Task<decimal> GetExpectedSalary(CancellationToken ct);
    
    public Task<decimal> GetActualSalary(CancellationToken ct);

    public Task<decimal> GetSalaryToday(CancellationToken ct);
}