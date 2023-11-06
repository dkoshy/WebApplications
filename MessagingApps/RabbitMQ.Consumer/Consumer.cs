
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Rabbit Mq consumer.");
var queeName = "mysamplequeue";
var rabbitmqFactory = new ConnectionFactory()
{
    HostName = "localhost",
    Port=5672
};

using var conn = rabbitmqFactory.CreateConnection();
using var channel = conn.CreateModel();

channel.QueueDeclare(queeName, false, false, false, null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    printMessage(message);
};
channel.BasicConsume(queeName,true, consumer);



 void printMessage(string message)
{
    Console.WriteLine("{0}", message);
}

Console.ReadLine();