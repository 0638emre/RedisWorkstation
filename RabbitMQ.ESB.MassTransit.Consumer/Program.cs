using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Cunsumers;

string rabbitMqUri = "amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp";

string queueName =  "exsmple-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
   factory.Host(rabbitMqUri);
   
   factory.ReceiveEndpoint(queueName, endpoint =>
   {
      endpoint.Consumer<ExampleMessageConsumer>();
   });
});

await bus.StartAsync();

Console.Read();