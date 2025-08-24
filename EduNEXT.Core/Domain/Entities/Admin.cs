using CSharpFunctionalExtensions;
using EduNEXT.Core.Domain.ValueObjects;

namespace EduNEXT.Core.Domain.Entities;

public class Admin : Entity<Guid>
{
    public Admin()
    {
        
    }
    
    public string Name { get; set; }
    
    public EmailAddress Email { get; set; }
    
    public string PasswordHash { get; set; }
}