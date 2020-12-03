using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumidor
{
    class ReceiveLogs
    {
        static void Main(string[] args)
        {
            //establecemos la conexión con server rabbitQm en localhost
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                //creamos intercambiador del tipo fanout => reevia todos los mensajes a todas las colas conocidas
                channel.ExchangeDeclare(exchange: "registros", type: ExchangeType.Fanout);

                //creamos cola aleatoria
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "registros",
                                  routingKey: "");

                Console.WriteLine("[*] Esperando registros.");

                //creamos consumidor
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] {message}");
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Presiona [intro] para salir.");
                Console.ReadLine();

            }
        }
    }
}
