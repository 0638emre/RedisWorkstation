using MassTransit;
using RabbitMQESB.MassTransit.WorkerService.Publisher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // services.AddHostedService<Worker>(); //burası default bir workerservice de geliyor. ilgisiz.
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
            });

            //oluşturduğumuz servisi buraya veriyoruz. Fakat Publisher MessageService içerisindeki  IPublishEndpoint interfaceinin register etmemiz gerekiyor yoksa uygulama ayağa kalkamaz IOC ye vermemiz gerekir.
            services.AddHostedService<PublishMessageService>(provider =>
            {
                using IServiceScope scope = provider.CreateScope();
                IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
                return new(publishEndpoint);
            });
        });
    })
    .Build();

host.Run();