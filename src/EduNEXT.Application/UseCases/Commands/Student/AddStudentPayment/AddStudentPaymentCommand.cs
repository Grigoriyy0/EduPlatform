using CSharpFunctionalExtensions;
using EduNEXT.Application.Dtos;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentPayment;

public class AddStudentPaymentCommand : IRequest<UnitResult<Error>>
{
    public AddPaymentDto dto { get; set; }
}