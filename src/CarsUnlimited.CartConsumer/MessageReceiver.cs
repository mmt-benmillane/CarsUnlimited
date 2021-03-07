using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarsUnlimited.CartConsumer
{
    public class MessageReceiver : DefaultBasicConsumer

    {

        private readonly IModel _channel;

        public MessageReceiver(IModel channel)

        {

            _channel = channel;

        }

        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine($"Consuming Message");
            Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
            Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
            Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
            Console.WriteLine(string.Concat("Routing tag: ", routingKey));

            var msgBody = body.ToArray();
            var message = Encoding.UTF8.GetString(msgBody);
            Console.WriteLine(string.Concat("Message: ", message));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5020/api/");
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-CartConsumerKey", "THIS_IS_MY_KEY");
                var completeCartTask = await client.PostAsync("complete-cart", new StringContent(message, Encoding.UTF8, "application/json"));

                if (completeCartTask != null)
                {
                    Console.WriteLine(completeCartTask.ToString());
                }
            }

            _channel.BasicAck(deliveryTag, false);
        }

    }
}
