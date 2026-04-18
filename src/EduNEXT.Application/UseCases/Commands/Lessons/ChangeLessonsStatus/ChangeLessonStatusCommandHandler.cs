using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Lessons.ChangeLessonsStatus;

public class ChangeLessonStatusCommandHandler(ILessonsRepository repository, IStudentRepository studentRepository)
    : IRequestHandler<ChangeLessonStatusCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(ChangeLessonStatusCommand request, CancellationToken ct)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, ct);

        if (lesson == null)
        {
            return ApplicationErrors.Lesson.LessonIsNotExists;
        }

        lesson.Complete();
        
        var student = await studentRepository.GetByIdAsync(lesson.StudentId, ct);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        var res = student.DecreasePaidLessonsCount();

        if (res.IsFailure)
        {
            return res.Error;
        }
        
        await studentRepository.UpdateAsync(student!, ct);
        await repository.UpdateAsync(lesson, ct);
        
        return UnitResult.Success<Error>();
    }
}