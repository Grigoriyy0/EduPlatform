using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommandHandler(ILessonsRepository repository, IStudentRepository studentRepository)
    : IRequestHandler<ChangeLessonStatusCommand, Result<Unit, Error>>
{
    public async Task<Result<Unit, Error>> Handle(ChangeLessonStatusCommand request, CancellationToken ct)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, ct);

        if (lesson == null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }

        lesson.Complete();
        
        
        
        var student = await studentRepository.GetStudentAsync(lesson.StudentId, ct);

        var studResult = Core.Domain.Entities.Student.DecreasePaidLessonsCount(student!, 1);

        if (studResult.IsFailure)
        {
            return studResult.Error;
        }

        student = studResult.Value;
        
        await studentRepository.UpdateAsync(student!, ct);
        await repository.UpdateAsync(lesson, ct);
        
        return Unit.Value;
    }
}