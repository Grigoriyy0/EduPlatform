using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentPayment;

public class AddStudentPaymentCommand : IRequest<UnitResult<Error>>
{
    public Guid StudentId { get; set; }
    
    public int PaidLessonsCount { get; set; }
}