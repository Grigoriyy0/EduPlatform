using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure.Persistence.Contexts;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public class SalaryRepository(MainContext context) : ISalaryRepository
{
    public Task<decimal> GetExpectedSalary()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetActualSalary()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetSalaryToday()
    {
        throw new NotImplementedException();
    }
}