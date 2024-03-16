// using System.Text;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;
//
// //Bağlantı oluşturma
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
//
// //Bağlantıyı Aktifleştirme ve Kanal Açma
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
// //Queue Oluşturma
// channel.QueueDeclare(queue:"example-queue",exclusive:false); //consumer da da kuyruk publishdaki ile birebir aynı yapılandırmada tanımlandırılmalıdı.r
//
// //Queueden mesaj kuma
// EventingBasicConsumer consumer = new(channel);
//
// channel.BasicConsume(queue:"example-queue", autoAck:false,consumer);
// //burada autoAck i false vererek consumerdan gidecek bildirimden, önce gelen mesajın silinmemesini sağlıyoruz.
//
// consumer.Received += (sender, e) =>
// {
//       //burası kuyruğa gelen mesajın işlendiği yerdir.
//       //e.Body :  Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
//       //e.Body.Span ve ya e.Body.ToArray() : kuyruktaki mesajın byte verisini getirecektir.
//       
//       Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//       //processs
//
//       //deliveryTag >  işte burada rabbit mq ya ben bu mesajı işleme aldım kuyruktan silebilirsin diyoruz. 
//       //multiple > sadece bu mesaja dair bir bildiride bulunacağımızı söylüyoruz. true olursa bundan önceki mesajları da sil demiş oluruz.
//       channel.BasicAck(deliveryTag:e.DeliveryTag, multiple:false);
//
// };

// ------ Direct Exchange Davranışı
//
// using System.Text;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;
//
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
// //bağlantı aktifleştirme ve kanal açma
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
// //1. adım 
// channel.ExchangeDeclare(exchange: "direkt-exchange-example", ExchangeType.Direct);
//
// //2.adım
// string queueName = channel.QueueDeclare().QueueName;
//
// //3.Adım
// channel.QueueBind(
//     queue:queueName,
//     exchange:"direkt-exchange-example",
//     routingKey:"direkt-queue-example");
//
// EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
// channel.BasicConsume(
//     queue: queueName,
//     autoAck: true,
//     consumer: consumer);
//
// consumer.Received += (sender, e) =>
// {
//     string message = Encoding.UTF8.GetString(e.Body.Span);
//     Console.WriteLine(message);
// };


//1. adım exchange publisher da ki exchange ile ismi tamamen aynı bir consumer tanımlıyoruz. 
//2.adım publisher tarafondan routing keyde bulunan değerdeki kuyruğa gönderilen mesajları, kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır.
//3.adım hazırlanan kuyruğa gelen mesajları okumak için bind olarak gelen her mesajı anlık olarak tüketiriz.s


//-----Fanout Exchange davranışı
// using System.Text;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;
//
// ConnectionFactory factory = new();
// // factory.Uri = new($"amqp://guest:guest@localhost:5672");
// factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");
//
// //bağlantı aktifleştirme ve kanal açma
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();
//
// channel.ExchangeDeclare("fanout-example", ExchangeType.Fanout);
//
// Console.WriteLine("Kuyruk adı giriniz : ");
//
// string queueName = Console.ReadLine();
//
// channel.QueueDeclare(
//     queue: queueName,
//     exclusive: false);
//
// channel.QueueBind(
//     queue: queueName, exchange: "fanout-example",  routingKey: String.Empty);
//
// EventingBasicConsumer consumer = new(channel);
//
// channel.BasicConsume(
//     queue: queueName,
//     autoAck: true,
//     consumer: consumer
// );
//
// consumer.Received += (sender, e) =>
// {
//     string message = Encoding.UTF8.GetString(e.Body.Span);
//     Console.WriteLine(message);
// };


    
Console.Read();





















