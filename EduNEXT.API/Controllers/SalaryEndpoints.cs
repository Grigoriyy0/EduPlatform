using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.API.Controllers;

public static class SalaryEndpoints
{
    public static void AddSalaryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/salary/", async (MainContext context) =>
        {
            var currentMonth = DateTime.Now.Month;
            
            var lessonsThisMonth = await context.Lessons.AsNoTracking()
                .Where(x => x.Date.Month == currentMonth)
                .Include(l => l.Student)
                .ToListAsync();
            
            return lessonsThisMonth.Sum(t =>
            {
                if (t.Student != null) return t.Student.LessonPrice;
                return 0;
            });
        });
    }
}