using System.Security.Cryptography;
using System.Text;
using EduNEXT.Application.Ports;

namespace EduNEXT.Infrastructure.Adapters;

public class DefaultHashProvider : IHashProvider
{
    private const int _keySize = 64;
    
    private const int _iterations = 1000;
    
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    
    public string ComputeHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_keySize); 
        
        var hash = Rfc2898DeriveBytes
            .Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize
                );
        
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public bool VerifyHash(string password, string hashedPassword)
    {
        var parts = hashedPassword.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }
}