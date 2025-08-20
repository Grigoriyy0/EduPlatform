using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Student.AddStudentCommand;

public class AddStudentCommandHandler(IMediator mediator, 
    IHashProvider hashProvider, 
    IPasswordGenerator passwordGenerator, 
    IStudentRepository studentRepository) : IRequestHandler<AddStudentCommand, Result<Core.Domain.Entities.Student, Error>>
{
    public async Task<Result<Core.Domain.Entities.Student, Error>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var password = passwordGenerator.GeneratePassword();
        
        var passwordHash = hashProvider.ComputeHash(password);
        
        var studentResult = Core.Domain.Entities.Student.Create(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Telegram ?? string.Empty, 
            request.SubscribedLessonsCount,
            request.PaidLessonsCount,
            request.LessonPrice,
            passwordHash);
        
        _ = studentResult.TryGetValue(out var student);

        if (studentResult.IsSuccess)
        {
            await studentRepository.AddStudentAsync(student);
        }
        
        return studentResult;
    }
}