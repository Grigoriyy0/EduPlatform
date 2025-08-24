using CSharpFunctionalExtensions;
using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Infrastructure.Options;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EduNEXT.Infrastructure.Adapters;

public class AuthManager : IAuthManager
{
    private readonly AuthManagerOptions _options;
    private readonly IHashProvider _hashProvider;
    private readonly MainContext _context;
    
    public AuthManager(IOptions<AuthManagerOptions> options, IHashProvider hashProvider, MainContext context)
    {
        _hashProvider = hashProvider;
        _context = context;
        _options = options.Value;
    }

    public async Task<Admin?> AuthAsync(string email, string password)
    {
        if (!string.Equals(_options.AuthorizedEmail, email))
        {
            return null;
        }
        
        var admin = await _context.Admins.FirstOrDefaultAsync(adm => adm.Email.Value ==  email);

        if (!_hashProvider.VerifyHash(password, admin!.PasswordHash))
        {
            return null;
        }
        
        return admin;
    } 
}
