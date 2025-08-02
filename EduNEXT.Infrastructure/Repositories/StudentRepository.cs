using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Repositories;

public sealed class StudentRepository(MainContext context) : IStudentRepository
{
    public async Task AddStudentAsync(Student student)
    { 
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
    }
    
    public Task<Student?> FindByIdAsync(Guid id)
    {
        return context.Students.AsNoTracking().
            FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task DeleteAsync(Student student)
    {
        context.Students.Remove(student);
        return context.SaveChangesAsync();
    }

    public Task UpdateAsync(Student student)
    {
        context.Students.Update(student);
        
        return context.SaveChangesAsync();
    }

    public Task<Student?> GetStudentAsync(Guid id)
    {
        return context.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var students = await context.Students.AsNoTracking().Include(s => s.LessonTimeSlots).ToListAsync();
        
        return students.Select(x => new StudentDto
        {
            Email = x.Email.Value,
            FirstName = x.Firstname,
            LastName = x.Lastname,
            LessonPrice = x.LessonPrice,
            PaidLessonsCount = x.PaidLessonsCount,
            SubscribedLessonsCount = x.SubscribedLessonsCount,
            StudentId = x.Id,
            TimeSlots = x.LessonTimeSlots.Select(y => new TimeSlotDto
            {
                Day = y.Day,
                StartTime = y.StartTime,
                EndTime = y.EndTime,
            }).ToList()
        }).ToList();
    }
}