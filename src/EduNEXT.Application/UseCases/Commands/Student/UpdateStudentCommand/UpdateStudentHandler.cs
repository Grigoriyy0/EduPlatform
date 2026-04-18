using CSharpFunctionalExtensions;
using EduNEXT.Application.Dtos;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using EduNEXT.Core.Domain.ValueObjects;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.UpdateStudentCommand;

public class UpdateStudentHandler(IStudentRepository studentRepository)
    : IRequestHandler<UpdateStudentCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(UpdateStudentCommand request, CancellationToken ct)
    {
        var student = await studentRepository.GetByIdAsync(request.dto.StudentId, ct);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        var updateResult = student.Update(request.dto.Name, request.dto.PaidLessonsCount, request.dto.SubscribedLessonsCount, request.dto.Telegram ?? string.Empty, request.dto.TimeZone, request.dto.LessonPrice);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }
        
        await studentRepository.UpdateAsync(student, ct);

        return UnitResult.Success<Error>();
    }
}