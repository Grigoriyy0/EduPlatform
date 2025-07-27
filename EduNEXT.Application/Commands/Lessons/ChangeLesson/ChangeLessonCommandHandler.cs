using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.Lessons.ChangeLesson;

public class ChangeLessonCommandHandler(ILessonsRepository repository)
    : IRequestHandler<ChangeLessonCommand, Result<Lesson, Error>>
{
    public async Task<Result<Lesson, Error>> Handle(ChangeLessonCommand request, CancellationToken cancellationToken)
    {
        var availability = await repository.CheckAvailableLessonAsync(
            request.Date,
            request.StartTime,
            request.EndTime);
        
        if()
    }
}