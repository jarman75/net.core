using RabbitMQ.Client;
using System;
using System.Text;

namespace Productor
{
    class EmitLog
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
                
                var mensaje = GetMessage(args);

                var body = Encoding.UTF8.GetBytes(mensaje);

                //publicamos el mensaje, en el intercambiador, sin especificar cola
                channel.BasicPublish(exchange: "registros",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Enviado {mensaje}");
                Console.ReadLine();
            }

            
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0)
                ? string.Join("", args)
                : "info: ¡Hola Mundo!");
        }
    }
}
