using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.RescheduleLesson;

public class RescheduleLessonCommandHandler(ILessonsRepository repository)
    : IRequestHandler<RescheduleLessonCommand, Result<Lesson, Error>>
{
    public async Task<Result<Lesson, Error>> Handle(RescheduleLessonCommand request, CancellationToken cancellationToken)
    {
        var interferedLessonGuids = await repository.GetInterferedLessonsAsync(
               request.Date,
               request.StartTime,
               request.EndTime);

        if (interferedLessonGuids.Count > 1 || interferedLessonGuids[0] != request.LessonId)
        { 
            return ApplicationErrors.Lesson.LessonTimeIsBooked;
        }
           
        var lesson = await repository.GetLessonAsync(request.LessonId);
        
        lesson = lesson!.UpdateLessonsTime(lesson, request.Date, request.StartTime, request.EndTime);
        
        await repository.UpdateLessonAsync(lesson);
        
        return lesson;
    }
}