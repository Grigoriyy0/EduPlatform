using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface IStudentRepository
{
    Task AddAsync(Student student, CancellationToken ct);
    
    Task DeleteAsync(Student student, CancellationToken ct);
    
    Task UpdateAsync(Student student, CancellationToken ct);
    
    Task<Student?> GetByIdAsync(Guid id, CancellationToken ct);
    
    Task<Student?> FindByIdAsync(Guid id, CancellationToken ct);
    
    Task<List<StudentDto>> GetAsync(CancellationToken ct);

    Task<int> GetCountAsync(CancellationToken ct);
}