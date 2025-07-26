using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Repositories;

public class StudentRepository(MainContext context) : IStudentRepository
{
    public async Task AddStudentAsync(Student student)
    { 
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
    }

    public Task<Student?> FindByIdAsync(Guid id)
    {
        return context.Students.
            AsNoTracking().
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
}