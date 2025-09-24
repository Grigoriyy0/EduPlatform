using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.ValueObjects;
using EduNEXT.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Configuration;

namespace EduNEXT.Infrastructure.Persistence.Utils;

public static class DbInitializer
{
    public static void Initialize(this MainContext context, IConfiguration config, IHashProvider hashProvider)
    {
        if (context.Admins.Any())
        {
            return;
        }
        
        var adminName = config["Admin:Name"];
        var adminPassword = config["Admin:Password"];
        
        var adminEmail = EmailAddress.Create(config["Admin:Email"]!).Value;
        
        var hashedPassword = hashProvider.ComputeHash(adminPassword!);

        var admin = new Admin
        {
            Name = adminName!,
            PasswordHash = hashedPassword,
            Email = adminEmail
        };

        context.Admins.Add(admin);
        context.SaveChanges();
    }
}