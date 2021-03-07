using CarsUnlimited.CartConsumer.Configuration;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CarsUnlimited.CartConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var serviceBusConfig = config.GetSection("ServiceBusConfiguration").Get<ServiceBusConfiguration>();

            ConnectionFactory connectionFactory = new()
            {
                HostName = serviceBusConfig.HostName,
                UserName = serviceBusConfig.UserName,
                Password = serviceBusConfig.Password
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cmd-cart",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                channel.BasicQos(0, 1, false);

                MessageReceiver messageReceiver = new(channel);

                channel.BasicConsume("cmd-cart", false, messageReceiver);

                Console.ReadLine();
            }
        }
    }
}
