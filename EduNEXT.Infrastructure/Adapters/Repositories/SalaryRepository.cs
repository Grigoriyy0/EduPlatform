using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public class SalaryRepository(MainContext context) : ISalaryRepository
{
    public async Task<decimal> GetExpectedSalary()
    {
        var currentMonth = DateTime.Now.Month;

        return await context.Lessons.AsNoTracking()
            .Where(x => x.Date.Month == currentMonth)
            .Include(l => l.Student)
            .SumAsync(l => l.Student != null ? l.Student.LessonPrice : 0);
    }

    public async Task<decimal> GetActualSalary()
    {
        var currentMonth = DateTime.Now.Month;
            
        return await context.Lessons.AsNoTracking()
            .Where(x => x.Date.Month == currentMonth && x.IsCompleted == true)
            .Include(l => l.Student)
            .SumAsync(l => l.Student != null ? l.Student.LessonPrice : 0);
    }

    public async Task<decimal> GetSalaryToday()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        return await context.Lessons.AsNoTracking()
            .Where(x => x.Date == today)
            .Include(l => l.Student)
            .SumAsync(t => t.Student != null ? t.Student.LessonPrice : 0);
    }
}