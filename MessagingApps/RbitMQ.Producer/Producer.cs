using RabbitMQ.Client;
using System.Text;

Console.WriteLine("RbbitMq Producer");
var queeName = "mysamplequeue";
var rabbitConnFactory = new ConnectionFactory()
{
    HostName = "localhost",
    Port = 5672
};

using var conn = rabbitConnFactory.CreateConnection();
using var channel = conn.CreateModel();
 channel.QueueDeclare("mysamplequeue", false, false, false, null);

var body = Encoding.UTF8.GetBytes("Getting started with .Net and RabbitMq");
channel.BasicPublish("", queeName, null, body);
Console.WriteLine( "Sent messages to ..{0}",queeName );

Console.ReadLine();