using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommandHandler(ILessonsRepository repository, IStudentRepository studentRepository)
    : IRequestHandler<ChangeLessonStatusCommand, Result<Unit, Error>>
{
    public async Task<Result<Unit, Error>> Handle(ChangeLessonStatusCommand request, CancellationToken cancellationToken)
    {
        var lesson = await repository.GetLessonAsync(request.LessonId);

        if (lesson == null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }

        lesson.Complete();
        
        
        
        var student = await studentRepository.GetStudentAsync(lesson.StudentId);

        var studResult = Core.Domain.Entities.Student.DecreasePaidLessonsCount(student!, 1);

        if (studResult.IsFailure)
        {
            return studResult.Error;
        }

        student = studResult.Value;
        
        await studentRepository.UpdateAsync(student!);
        await repository.UpdateLessonAsync(lesson);
        
        return Unit.Value;
    }
}