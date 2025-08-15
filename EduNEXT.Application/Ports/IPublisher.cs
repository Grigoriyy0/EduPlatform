namespace EduNEXT.Application.Ports;

public interface IPublisher
{
    public Task SendMessageAsync(string message);

    public Task SendToQueueAsync(string message);
}