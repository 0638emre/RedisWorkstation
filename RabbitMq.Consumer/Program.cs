using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new();
// factory.Uri = new($"amqp://guest:guest@localhost:5672");
factory.Uri = new("amqps://uulwhncp:M9EHb7NXfucL4JiXSzOl_j2TuBnMTA_6@turkey.rmq.cloudamqp.com/uulwhncp");


//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue:"example-queue",exclusive:false); //consumer da da kuyruk publishdaki ile birebir aynı yapılandırmada tanımlandırılmalıdı.r

//Queueden mesaj kuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:"example-queue", false,consumer);
consumer.Received += (sender, e) =>
{
      //burası kuyruğa gelen mesajın işlendiği yerdir.
      //e.Body :  Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
      //e.Body.Span ve ya e.Body.ToArray() : kuyruktaki mesajın byte verisini getirecektir.
      
      Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};