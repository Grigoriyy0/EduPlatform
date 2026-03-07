using EduNEXT.Application.Dtos.Analytics;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Students.GetAnalyticsData;

public class GetStudentAnalyticsHandler(IStudentRepository repository)
    : IRequestHandler<GetStudentAnalyticsQuery, StudentAnalyticsDto>
{
    public async Task<StudentAnalyticsDto> Handle(GetStudentAnalyticsQuery request, CancellationToken ct)
    {
        var studentsCount = await repository.GetCountAsync(ct);

        return new StudentAnalyticsDto
        {
            StudentsCount = studentsCount
        };
    }
}