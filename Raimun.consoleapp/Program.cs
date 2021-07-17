using EventContracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MassTransit;
using RabbitMQ.Client.Events;
using System.Text;

namespace EventContracts
{
    public interface ValueEntered
    {
        string Value { get; }
    }
}

namespace Raimun.consoleapp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //        Console.ReadLine();
    //    }
    //}

    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine(" waiting.");
            //Console.ReadLine();

            //var factory = new ConnectionFactory() { HostName = "rabbitmq", UserName= "guest" , Password= "guest" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "weatherqueue",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body.ToArray();
            //        var message = Encoding.UTF8.GetString(body);
            //        Console.WriteLine(" [x] Received {0}", message);
            //    };
            //    channel.BasicConsume(queue: "weatherqueue",
            //                         autoAck: true,
            //                         consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            //    Console.ReadLine();
            //}

            Console.WriteLine(DateTime.Now.ToString());
            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(new Uri("rabbitmq://rabbitmq"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });


                config.ReceiveEndpoint("weatherqueue", e =>
                {
                    e.Consumer<WeatherDataConsumer>();
                });

            });

            //busControl.ConnectConsumer<WeatherDataConsumer>();
            await busControl.StartAsync();


            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
            await busControl.StopAsync();

        }
    }
}
