using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Salary.GetActual;

public class GetActualSalaryHandler(ISalaryRepository repository) : IRequestHandler<GetActualSalaryQuery, decimal>
{
    public async Task<decimal> Handle(GetActualSalaryQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetActualSalary();
    }
}