using EduNEXT.Application.Ports;
using MassTransit;

namespace EduNEXT.Infrastructure.Adapters;

public class Publisher : IPublisher
{
    private IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    
    public Publisher(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task SendMessageAsync(string message)
    {
        var publishMessage = new Notification(message);
        
        await _publishEndpoint.Publish(publishMessage);
    }
    
    public async Task SendToQueueAsync(string message)
    {
        
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:telegram-notification-queue"));
        
        await endpoint.Send(new Notification(message));
    }
}