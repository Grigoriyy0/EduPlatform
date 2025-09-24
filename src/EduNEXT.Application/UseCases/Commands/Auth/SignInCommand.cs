using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using MediatR;
using Primitives;

namespace EduNEXT.Application.UseCases.Commands.Auth;

public class SignInCommand : IRequest<Result<string, Error>>
{
    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}