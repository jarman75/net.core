using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace ProductorDirecto
{
    class EmitLogDirect
    {
        static void Main(string[] args)
        {

            //establecemos la conexión con server rabbitQm en localhost
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //creamos intercambiador del tipo direct
                channel.ExchangeDeclare(exchange: "direct_logs", 
                                        type: ExchangeType.Direct);

                var severity = (args.Length > 0) ? args[0] : "info";
                var message = GetMessage(args);

                var body = Encoding.UTF8.GetBytes(message);

                //publicamos el mensaje, en el intercambiador, key enrutado = severity
                channel.BasicPublish(exchange: "direct_logs",
                                     routingKey: severity,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Sent '{severity}':'{message}'");
                
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 1)
                ? string.Join("", args.Skip(1).ToArray())
                : "Hello World!");
        }
    }
}
