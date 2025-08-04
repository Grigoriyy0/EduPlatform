using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Queries.Lessons.GetAllLessonsByFilter;

public class GetAllLessonsByFilterQueryHandler(ILessonsRepository repository)
    : IRequestHandler<GetAllLessonsByFilterQuery, IList<Lesson>>
{
    public async Task<IList<Lesson>> Handle(GetAllLessonsByFilterQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetLessonsAsync(request.CriteriaName);
    }
}