using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.CancelLessonCommand;

public class CancelLessonCommandHandler(ILessonsRepository repository)
    : IRequestHandler<CancelLessonCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(CancelLessonCommand request, CancellationToken ct)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, ct);

        if (lesson == null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }
        
        await repository.DeleteAsync(lesson, ct);

        return UnitResult.Success<Error>();
    }
}