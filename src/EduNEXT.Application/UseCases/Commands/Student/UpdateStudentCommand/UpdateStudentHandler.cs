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
    public async Task<UnitResult<Error>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetStudentAsync(request.dto.StudentId);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        student.Name = request.dto.Name;
        
        student.PaidLessonsCount = request.dto.PaidLessonsCount;
        student.SubscribedLessonsCount = request.dto.SubscribedLessonsCount;

        student.Telegram = request.dto.Telegram;
        student.TimeZone = request.dto.TimeZone;
        
        if (request.dto.LessonPrice <= 0)
        {
            return ApplicationErrors.Student.StudentLessonPriceIsIncorrect;
        }
        student.LessonPrice = request.dto.LessonPrice;
        
        await studentRepository.UpdateAsync(student);

        return UnitResult.Success<Error>();
    }
}