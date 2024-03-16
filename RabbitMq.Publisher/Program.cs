// using System.Text;
// using RabbitMQ.Client;
//
// //rabbit mq bağlantı oluşturma
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
// //bağlantı aktifleştirme ve kanal açma
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
// //Queue oluşturma
// channel.QueueDeclare(queue:"example-queue",exclusive:false);
// //not: queue kuyruk adı - exclusive : başka bir bağlantı tarafından kullanılmaması için false.
//
// //Queue'ya mesaj gönderme
// //Not: rabbit mq kuyrupa gönderilen mesajları byte olarak kabul etmektedir.
// byte[] message = Encoding.UTF8.GetBytes("Merhaba emre");
// channel.BasicPublish(exchange: "", routingKey:"example-queue", body:message);
//
// Console.Read();
//  

// ------ Direct Exchange Davranışı

// using System.Text;
// using RabbitMQ.Client;
//
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
//
// channel.ExchangeDeclare(exchange: "direkt-exchange-example", ExchangeType.Direct);
//
// while (true)
// {
//     Console.Write("Mesaj : ");
//     string message = Console.ReadLine();
//
//     byte[] byteMessage = Encoding.UTF8.GetBytes(message);
//
//     channel.BasicPublish(
//         exchange: "direkt-exchange-example", 
//         routingKey: "direkt-queue-example",
//         body: byteMessage);
// }

//------Fanout Exchange Davranışı
// using System.Text;
// using RabbitMQ.Client;
//
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
// channel.ExchangeDeclare("fanout-example", ExchangeType.Fanout);
//
// for (int i = 0; i < 100; i++)
// {
//     await Task.Delay(2000);
//     byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
//     channel.BasicPublish(
//         exchange:"fanout-example",
//         routingKey:String.Empty,
//         body:message);
// }




Console.Read();


















