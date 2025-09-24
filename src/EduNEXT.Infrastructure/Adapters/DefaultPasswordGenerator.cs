using System.Text;
using EduNEXT.Application.Ports;

namespace EduNEXT.Infrastructure.Adapters;

public class DefaultPasswordGenerator : IPasswordGenerator
{
    private static readonly Random Random = new Random();
    
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private const int PasswordLength = 10;
    
    public string GeneratePassword()
    {
        var stringBuilder = new StringBuilder(PasswordLength);

        for (int i = 0; i < PasswordLength; i++)
        {
            stringBuilder.Append(Chars[Random.Next(0, Chars.Length)]);
        }
        
        return stringBuilder.ToString();
    }
}