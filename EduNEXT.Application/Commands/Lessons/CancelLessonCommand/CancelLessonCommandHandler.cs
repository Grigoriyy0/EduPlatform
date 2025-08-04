using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.CancelLessonCommand;

public class CancelLessonCommandHandler(ILessonsRepository repository)
    : IRequestHandler<CancelLessonCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(CancelLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await repository.GetLessonAsync(request.LessonId);

        if (lesson == null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }
        
        await repository.DeleteLessonAsync(lesson);

        return UnitResult.Success<Error>();
    }
}