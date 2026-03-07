using EduNEXT.Application.Ports;
using MassTransit;

namespace EduNEXT.Infrastructure.Messaging;

public class Publisher : IPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    
    public Publisher(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task SendMessageAsync(string message, CancellationToken ct)
    {
        var publishMessage = new Notification(message);
        
        await _publishEndpoint.Publish(publishMessage, ct);
    }
    
    public async Task SendToQueueAsync(string message, CancellationToken ct)
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:telegram-notification-queue"));
        
        await endpoint.Send(new Notification(message), ct);
    }
}