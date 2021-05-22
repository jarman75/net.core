using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using static ConsumerMessages.Show.Worker;

namespace ConsumerMessages.Show
{
    public class Program
    {   
        

        public static void Main(string[] args)
        {
            CreateConsumer();

            CreateHostBuilder(args).Build().Run();
        }

        public static void CreateConsumer()
        {
            
            ConnectionFactory connectionFactory = new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();            
            channel.ExchangeDeclare(exchange: "messages", type: ExchangeType.Fanout);
            
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "messages",
                              routingKey: "");

            // accept only one unack-ed message at a time
            // uint prefetchSize, ushort prefetchCount, bool global
            channel.BasicQos(0, 1, false);
            MessageReceiver messageReceiver = new MessageReceiver(channel);
            channel.BasicConsume(queueName, false, messageReceiver);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
