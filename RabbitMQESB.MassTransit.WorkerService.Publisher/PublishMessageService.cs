using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQESB.MassTransit.WorkerService.Publisher;

public class PublishMessageService : BackgroundService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PublishMessageService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;

        while (true)
        {
            ExampleMessage message = new()
            {
                Text = $"{++i}. mesaj"
            };

            await _publishEndpoint.Publish(message);
        }
    }
}