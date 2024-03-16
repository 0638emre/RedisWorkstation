using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

namespace RabbitMQESB.MassTransit.WorkerService.Consumer.Consumer;

public class ExampleMessageConsumer : IConsumer<IMessage>
{
    public async Task Consume(ConsumeContext<IMessage> context)
    {
        Console.WriteLine($"Gelen mesaj : {context.Message.Text}");

        await Task.CompletedTask;
    }
}