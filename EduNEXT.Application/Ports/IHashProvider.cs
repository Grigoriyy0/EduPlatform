namespace EduNEXT.Application.Ports;

public interface IHashProvider
{
    public string ComputeHash(string password);
    
    public bool VerifyHash(string password, string hash);
}