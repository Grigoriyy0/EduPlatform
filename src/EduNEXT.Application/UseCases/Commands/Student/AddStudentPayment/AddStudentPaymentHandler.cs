using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentPayment;

public class AddStudentPaymentHandler(IStudentRepository studentRepository) : IRequestHandler<AddStudentPaymentCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(AddStudentPaymentCommand request, CancellationToken ct)
    {
        var student = await studentRepository.GetByIdAsync(request.dto.StudentId, ct);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        var result = student.AddPaidLessons(request.dto.Amount);

        if (result.IsFailure)
        {
            return result.Error;
        }
        
        await studentRepository.UpdateAsync(student, ct);

        return UnitResult.Success<Error>();
    }
}