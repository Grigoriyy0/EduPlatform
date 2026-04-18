using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public sealed class SalaryRepository(MainContext context) : ISalaryRepository
{
    public Task<decimal> GetExpectedAsync(CancellationToken ct)
    {
        var currentMonth = DateTime.Now.Month;

        return context.Lessons.AsNoTracking()
            .Where(x => x.Date.Month == currentMonth)
            .Include(l => l.Student)
            .SumAsync(l => l.Student != null ? l.Student.LessonPrice : 0, ct);
    }

    public Task<decimal> GetActualAsync(CancellationToken ct)
    {
        var currentMonth = DateTime.Now.Month;
            
        return context.Lessons.AsNoTracking()
            .Where(x => x.Date.Month == currentMonth && x.IsCompleted)
            .Include(l => l.Student)
            .SumAsync(l => l.Student != null ? l.Student.LessonPrice : 0, ct);
    }
}