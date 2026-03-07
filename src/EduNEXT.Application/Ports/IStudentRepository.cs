using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student, CancellationToken ct);
    
    Task<Student?> FindByIdAsync(Guid id, CancellationToken ct);
    
    Task DeleteAsync(Student student, CancellationToken ct);
    
    Task UpdateAsync(Student student, CancellationToken ct);
    
    Task<Student?> GetStudentAsync(Guid id, CancellationToken ct);
    
    Task<List<StudentDto>> GetAllStudentsAsync(CancellationToken ct);

    Task<int> GetStudentsCountAsync(CancellationToken ct);
}