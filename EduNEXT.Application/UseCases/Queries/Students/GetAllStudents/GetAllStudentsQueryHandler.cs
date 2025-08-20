using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Students.GetAllStudents;

public class GetAllStudentsQueryHandler(IStudentRepository repository)
    : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllStudentsAsync();
    }
}