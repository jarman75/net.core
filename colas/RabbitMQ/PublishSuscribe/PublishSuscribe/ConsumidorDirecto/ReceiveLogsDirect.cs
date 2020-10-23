using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsumidorDirecto
{
    class ReceiveLogsDirect
    {
        static void Main(string[] args)
        {
            //establecemos la conexión con server rabbitQm en localhost
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //creamos intercambiador del tipo direct
                channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                //creamos cola aleatoria
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                            Environment.GetCommandLineArgs()[0]);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var severity in args)
                {
                    channel.QueueBind(queue: queueName,
                                  exchange: "direct_logs",
                                  routingKey: severity);
                }
                
                

                Console.WriteLine("[*] Waiting for messages.");

                //creamos consumidor
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine($"[x] Received '{routingKey}':{message}");
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }
        }
    }
}
