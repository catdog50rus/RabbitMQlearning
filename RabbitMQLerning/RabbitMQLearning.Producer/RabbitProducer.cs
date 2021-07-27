using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQLearning.Producer
{
    public class RabbitProducer
    {
        private readonly ConnectionFactory factory;

        public RabbitProducer()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
        }


        public bool SendMessage(string message)
        {
            try
            {
                Thread.Sleep(1500);
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "test-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "test-queue",
                                     basicProperties: null,
                                     body: body);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
