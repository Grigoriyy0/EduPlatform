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
    public async Task<Result<Core.Domain.Entities.Student, Error>> Handle(AddStudentCommand request, CancellationToken ct)
    {
        var password = passwordGenerator.GeneratePassword();
        
        var passwordHash = hashProvider.ComputeHash(password);
        
        var studentResult = Core.Domain.Entities.Student.Create(
            request.Name, 
            request.TimeZone,
            request.Telegram ?? string.Empty, 
            request.PaidLessonsCount,
            request.SubscribedLessonsCount,
            request.LessonPrice,
            passwordHash);

        if (studentResult.IsSuccess)
        {
            await studentRepository.AddAsync(studentResult.Value, ct);
        }
        
        return studentResult;
    }
}