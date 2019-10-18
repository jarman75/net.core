﻿using RabbitMQ.Client;
using System;
using System.Text;

namespace NewTask
{
    class NewTask
    {
        public static void Main(string[] args)
        {
            
            

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(exchange: "", routingKey: "task_queue", basicProperties: properties, body: body);

                Console.WriteLine(" [x] Send {0}", message);
            }
            

        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
