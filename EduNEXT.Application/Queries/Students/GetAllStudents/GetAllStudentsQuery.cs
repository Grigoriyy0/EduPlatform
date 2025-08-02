using EduNEXT.Application.Dtos;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Queries.Students.GetAllStudents;

public record class GetAllStudentsQuery : IRequest<List<StudentDto>> {}