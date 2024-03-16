using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

string rabbitMqUri = "amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp";

string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMqUri}/request-queue"));

int i = 1;

while (true)
{
    await Task.Delay(300);
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i++}. request" });

    Console.WriteLine($"Response received: {response.Message.Text}");
}