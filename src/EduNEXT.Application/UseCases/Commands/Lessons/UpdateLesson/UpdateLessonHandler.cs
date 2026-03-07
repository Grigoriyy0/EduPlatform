using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.UpdateLesson;

public class UpdateLessonHandler(
    ILessonsRepository lessonsRepository
    ) : IRequestHandler<UpdateLessonCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await lessonsRepository.GetByIdAsync(request.LessonId);

        if (lesson is null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }

        var interferedLessonGuids = await lessonsRepository.GetInterferedLessonsAsync(
            request.Date,
            request.StartTime,
            request.EndTime);

        if (interferedLessonGuids.Count > 1 || interferedLessonGuids[0] != request.LessonId)
        { 
            return ApplicationErrors.Lesson.LessonTimeIsBooked;
        }
        
        var updateResult = lesson.Update(request.Date, request.StartTime, request.EndTime, request.IsCompleted);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }
        
        await lessonsRepository.UpdateAsync(lesson);

        return UnitResult.Success<Error>();
    }
}