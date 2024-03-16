using MassTransit;
using RabbitMQESB.MassTransit.WorkerService.Consumer;
using RabbitMQESB.MassTransit.WorkerService.Consumer.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // services.AddHostedService<Worker>(); //default workerservice de geliyor.
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();
            
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
                
                _configurator.ReceiveEndpoint("example-message-queue", e=> e.ConfigureConsumer<ExampleMessageConsumer>(context) );
            });
            
            

        });
        
    })
    .Build();

host.Run();