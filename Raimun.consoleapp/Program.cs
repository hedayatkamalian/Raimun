using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MassTransit;
using RabbitMQ.Client.Events;
using System.Text;


namespace Raimun.consoleapp
{

    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine(" waiting.");
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

            await busControl.StartAsync();


            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
            await busControl.StopAsync();

        }
    }
}
