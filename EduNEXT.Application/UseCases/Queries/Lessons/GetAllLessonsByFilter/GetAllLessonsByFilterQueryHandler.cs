using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Lessons.GetAllLessonsByFilter;

public class GetAllLessonsByFilterQueryHandler(ILessonsRepository repository)
    : IRequestHandler<GetAllLessonsByFilterQuery, IList<LessonDto>>
{
    public async Task<IList<LessonDto>> Handle(GetAllLessonsByFilterQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetLessonsAsync(request.CriteriaName);
    }
}