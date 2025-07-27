using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student);
    
    Task<Student?> FindByIdAsync(Guid id);
    
    Task DeleteAsync(Student student);
    
    Task UpdateAsync(Student student);
    
    Task<Student?> GetStudentAsync(Guid id);
}