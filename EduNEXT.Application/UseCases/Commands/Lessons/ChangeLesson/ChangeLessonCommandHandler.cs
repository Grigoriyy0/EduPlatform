using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.ChangeLesson;

public class ChangeLessonCommandHandler(ILessonsRepository repository)
    : IRequestHandler<ChangeLessonCommand, Result<Lesson, Error>>
{
    public async Task<Result<Lesson, Error>> Handle(ChangeLessonCommand request, CancellationToken cancellationToken)
    {
        var availabilityFlag = await repository.CheckAvailableLessonAsync(
            request.Date,
            request.StartTime,
            request.EndTime);

        if (availabilityFlag)
        {
            return ApplicationErrors.Lesson.LessonTimeIsBooked;
        }
        
        var lesson = await repository.GetLessonAsync(request.LessonId);
        
        lesson = lesson!.UpdateLessonsTime(lesson, request.Date, request.StartTime, request.EndTime);
        
        await repository.UpdateLessonAsync(lesson);
        
        return lesson;
    }
}