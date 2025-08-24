using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface IAuthManager
{
    public Task<Admin?> AuthAsync(string email, string password);
}