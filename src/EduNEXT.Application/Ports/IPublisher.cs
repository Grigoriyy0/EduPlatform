namespace EduNEXT.Application.Ports;

public interface IPublisher
{
    public Task SendMessageAsync(string message, CancellationToken ct);

    public Task SendToQueueAsync(string message, CancellationToken ct);
}