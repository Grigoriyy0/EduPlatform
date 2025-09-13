using EduNEXT.Application.Dtos.Analytics;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Students.GetAnalyticsData;

public class GetStudentAnalyticsHandler(IStudentRepository repository)
    : IRequestHandler<GetStudentAnalyticsQuery, StudentAnalyticsDto>
{
    public async Task<StudentAnalyticsDto> Handle(GetStudentAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var studentsCount = await repository.GetStudentsCountAsync();

        return new StudentAnalyticsDto
        {
            StudentsCount = studentsCount
        };
    }
}