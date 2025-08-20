using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Salary.GetExpected;

public class GetExpectedSalaryHandler(ISalaryRepository repository)
    : IRequestHandler<GetExpectedSalaryQuery, decimal>
{
    public async Task<decimal> Handle(GetExpectedSalaryQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetExpectedSalary();
    }
}