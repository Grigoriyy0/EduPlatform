using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Errors;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Auth;

public class SignInCommandHandler(
    IAuthManager authManager, 
    ITokenProducer tokenProducer
    )
    : IRequestHandler<SignInCommand, Result<string, Error>>
{
    public async Task<Result<string, Error>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var admin = await authManager.AuthAsync(request.Email, request.Password);

        if (admin == null)
        {
            return ApplicationErrors.Auth.UserIsNotAdmin;
        }

        var token = tokenProducer.ProduceToken(admin);
        
        return token;
    }
}