namespace EduNEXT.Application.Ports;

public interface ISalaryRepository
{
    public Task<decimal> GetExpectedAsync(CancellationToken ct);
    
    public Task<decimal> GetActualAsync(CancellationToken ct);
}