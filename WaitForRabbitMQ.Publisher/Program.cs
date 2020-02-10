using System;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace WaitForRabbitMQ.Publisher
{
    public static class Program
    {
        public static void Main()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var host = config["HostName"];

            Console.WriteLine($"Using host {host} to publish message");

            // ENV VARIABLE DOESN'T WORK PROPERLY
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine(" [x] Sent {0}", message);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
