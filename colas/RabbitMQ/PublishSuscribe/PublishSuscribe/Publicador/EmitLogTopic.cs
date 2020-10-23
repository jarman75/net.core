using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace Publicador
{
    class EmitLogTopic
    {
        static void Main(string[] args)
        {

            //establecemos la conexión con server rabbitQm en localhost
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //creamos intercambiador del tipo topic (temas)
                channel.ExchangeDeclare(exchange: "topic_logs",
                                        type: ExchangeType.Topic);

                var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
                var message = GetMessage(args);

                var body = Encoding.UTF8.GetBytes(message);

                //publicamos el mensaje, en el intercambiador, key enrutado = routingKey
                channel.BasicPublish(exchange: "topic_logs",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Sent '{routingKey}':'{message}'");

            }

        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 1)
                ? string.Join("", args.Skip(1).ToArray())
                : "Hello World!");
        }
    }
}
