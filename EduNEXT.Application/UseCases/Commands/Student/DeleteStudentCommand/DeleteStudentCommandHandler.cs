using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.DeleteStudentCommand;

public class DeleteStudentCommandHandler(IStudentRepository studentRepository)
    : IRequestHandler<DeleteStudentCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await studentRepository.GetStudentAsync(request.Id);

        if (student == null)
        {
            return ApplicationErrors.Student.StudentIsNotExists;
        }
        
        await studentRepository.DeleteAsync(student);
        
        return new UnitResult<Error>();
    }
}