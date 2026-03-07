using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.DeleteStudentCommand;

public class DeleteStudentCommandHandler(IStudentRepository studentRepository)
    : IRequestHandler<DeleteStudentCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(DeleteStudentCommand request, CancellationToken ct)
    {
        var student = await studentRepository.GetStudentAsync(request.Id, ct);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        await studentRepository.DeleteAsync(student, ct);
        
        return new UnitResult<Error>();
    }
}