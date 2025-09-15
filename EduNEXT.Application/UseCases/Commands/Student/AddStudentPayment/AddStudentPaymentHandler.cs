using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentPayment;

public class AddStudentPaymentHandler(IStudentRepository studentRepository) : IRequestHandler<AddStudentPaymentCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(AddStudentPaymentCommand request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetStudentAsync(request.StudentId);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        student.PaidLessonsCount +=  request.PaidLessonsCount;
        
        await studentRepository.UpdateAsync(student);

        return UnitResult.Success<Error>();
    }
}