using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Ports;

public interface ITokenProducer
{
    public string ProduceToken(Admin admin);
}