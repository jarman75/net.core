using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Service.Core;
using System;
using System.Text;
using System.Text.Json;

namespace ConsumerMessages.Show
{
    public partial class Worker
    {
        public class MessageReceiver : DefaultBasicConsumer
        {
            private readonly IModel _channel;            

            public MessageReceiver(IModel channel)
            {
                _channel = channel;                
            }

            public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
            {
                Console.WriteLine($"Consuming fanout Message");
                Console.WriteLine($"Message received from the exchange {exchange}");
                Console.WriteLine($"Consumer tag: {consumerTag}");
                Console.WriteLine($"Delivery tag: {deliveryTag}");
                Console.WriteLine($"Routing tag: {routingKey}");
                
                var message = body.ToArray();
                var data = JsonSerializer.Deserialize<Message>(Encoding.UTF8.GetString(message));

                Console.WriteLine($"Subject: {data.Subject} - Message: {data.Text}");

                _channel.BasicAck(deliveryTag, false);
            }
            
        }
        
    }    
}
