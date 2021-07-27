﻿using RabbitMQ.Client;
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
            channel.ExchangeDeclare("dev-direct", ExchangeType.Direct);

            var queueName = channel.QueueDeclare();

            channel.QueueBind(queue: queueName,
                              exchange: "dev-direct",
                              routingKey: "error");


            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: queueName,
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
