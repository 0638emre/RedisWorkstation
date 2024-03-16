using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMqUri = "amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.WriteLine("Gönderilecek mesaj : ");

string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});

Console.Read();