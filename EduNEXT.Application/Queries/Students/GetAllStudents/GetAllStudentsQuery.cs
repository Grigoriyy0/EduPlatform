using EduNEXT.Application.Dtos;
using MediatR;

namespace EduNEXT.Application.Queries.Students.GetAllStudents;

public record GetAllStudentsQuery : IRequest<List<StudentDto>> {}