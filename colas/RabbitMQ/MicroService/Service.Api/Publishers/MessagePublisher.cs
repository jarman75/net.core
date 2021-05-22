using RabbitMQ.Client;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Api.Publishers
{
    public class MessagePublisher
    {

        private const string UName = "guest";
        private const string PWD = "guest";
        private const string HName = "localhost";

        public void SendMessage(Message message)
        {
            //establecemos la conexión con server rabbitQm en localhost
            var factory = new ConnectionFactory() { UserName = UName, Password = PWD, HostName = HName };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = false;

            //creamos intercambiador del tipo fanout => reevia todos los mensajes a todas las colas conocidas
            channel.ExchangeDeclare(exchange: "messages", type: ExchangeType.Fanout);

            var dataText = JsonSerializer.Serialize(message, typeof(Message));
            var body = Encoding.UTF8.GetBytes(dataText);

            //publicamos el mensaje, en el intercambiador, sin especificar cola
            channel.BasicPublish(exchange: "messages",
                                    routingKey: "",
                                    basicProperties: properties,
                                    body: body);                
            
        }
    }
}
