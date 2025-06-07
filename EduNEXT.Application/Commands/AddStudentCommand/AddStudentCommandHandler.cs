using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.ValueObjects;
using MediatR;
using Primitives;

namespace EduNEXT.Application.Commands.AddStudentCommand;

public class AddStudentCommandHandler(IMediator mediator) : IRequestHandler<AddStudentCommand, Result<Student, Error>>
{
    private readonly IMediator mediator = mediator;

    public async Task<Result<Student, Error>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Telegram ?? string.Empty, 
            request.PaidLessonsCount,
            request.LessonPrice);
        
        return student;
    }
}