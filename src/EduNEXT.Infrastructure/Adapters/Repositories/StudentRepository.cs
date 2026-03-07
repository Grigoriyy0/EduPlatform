using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Adapters.Repositories;

public sealed class StudentRepository(MainContext context) : IStudentRepository
{
    public async Task AddStudentAsync(Student student, CancellationToken ct)
    { 
        await context.Students.AddAsync(student, ct);
        await context.SaveChangesAsync(ct);
    }
    
    public Task<Student?> FindByIdAsync(Guid id, CancellationToken ct)
    {
        return context.Students.AsNoTracking().
            FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public Task DeleteAsync(Student student, CancellationToken ct)
    {
        context.Students.Remove(student);
        return context.SaveChangesAsync(ct);
    }

    public Task UpdateAsync(Student student, CancellationToken ct)
    {
        context.Students.Update(student);
        
        return context.SaveChangesAsync(ct);
    }

    public Task<Student?> GetStudentAsync(Guid id, CancellationToken ct)
    {
        return context.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync(CancellationToken ct)
    {
        return await context.Students
            .AsNoTracking()
            .Select(x => new StudentDto
            {
                StudentId = x.Id,
                Name = x.Name,
                TimeZone = x.TimeZone,
                Telegram = x.Telegram ?? string.Empty,
                LessonPrice = x.LessonPrice,
                PaidLessonsCount = x.PaidLessonsCount,
                SubscribedLessonsCount = x.SubscribedLessonsCount,
                TimeSlots = x.LessonTimeSlots
                    .Select(y => new TimeSlotDto
                    {
                        Day = y.Day,
                        StartTime = y.StartTime,
                        EndTime = y.EndTime
                    })
                    .ToList()
            })
            .ToListAsync(ct);
    }

    public Task<int> GetStudentsCountAsync(CancellationToken ct)
    {
        return context.Students.CountAsync(ct);
    }
}