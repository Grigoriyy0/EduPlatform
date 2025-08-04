using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Queries.Lessons.GetAllLessonsByFilter;

public class GetAllLessonsByFilterQuery : IRequest<IList<Lesson>>
{
    public string CriteriaName { get; set; }
}