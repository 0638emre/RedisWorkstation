using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponse.Consumer.Consumers;

string rabbitMqUri = "amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp";

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
   cfg.Host(rabbitMqUri);
   
   cfg.ReceiveEndpoint("request-queue", endpoint =>
   {
      endpoint.Consumer<RequestMessageConsumer>();
   });
});

await bus.StartAsync();

Console.Read();