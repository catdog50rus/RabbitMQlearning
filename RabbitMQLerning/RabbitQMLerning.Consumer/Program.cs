using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RabbitQMLerning.Consumer
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "test-queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(queue: "test-queue",
                                     autoAck: true,
                                     consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Получено сообщение: {message}");
                };

            Console.ReadKey();
        }
    }
}
