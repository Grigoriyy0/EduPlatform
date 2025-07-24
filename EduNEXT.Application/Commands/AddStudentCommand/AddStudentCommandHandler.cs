using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.AddStudentCommand;

public class AddStudentCommandHandler(IMediator mediator, IHashProvider hashProvider, IPasswordGenerator passwordGenerator) : IRequestHandler<AddStudentCommand, Result<Student, Error>>
{
    private readonly IMediator _mediator = mediator;

    public async Task<Result<Student, Error>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var password = passwordGenerator.GeneratePassword();
        
        var passwordHash = hashProvider.ComputeHash(password);
        
        var student = Student.Create(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Telegram ?? string.Empty, 
            request.SubscribedLessonsCount,
            request.PaidLessonsCount,
            request.LessonPrice,
            passwordHash);
        
        return student;
    }
}