using EduNEXT.Application.Dtos;
using MediatR;

namespace EduNEXT.Application.UseCases.Queries.Lessons.GetAllLessonsByFilter;

public class GetAllLessonsByFilterQuery : IRequest<IList<LessonDto>>
{
    public string CriteriaName { get; set; }
}