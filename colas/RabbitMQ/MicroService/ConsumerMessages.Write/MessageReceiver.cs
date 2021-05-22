using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Service.Core;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ConsumerMessages.Write
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

                var message = body.ToArray();

                var data = JsonSerializer.Deserialize<Message>(Encoding.UTF8.GetString(message));

                var dataText = JsonSerializer.Serialize(data, typeof(Message));

                File.WriteAllText($"{data.Id}.json", dataText);

                _channel.BasicAck(deliveryTag, false);
            }

        }

    }
}
