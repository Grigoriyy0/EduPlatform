using EduNEXT.Application.Dtos;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Students.GetAllStudents;

public record GetAllStudentsQuery : IRequest<List<StudentDto>> {}