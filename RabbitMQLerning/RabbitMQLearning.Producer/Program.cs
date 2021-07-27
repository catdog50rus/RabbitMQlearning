using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQLearning.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(CreatTask(8000, "error"));
            Task.Run(CreatTask(1000, "info"));
            Task.Run(CreatTask(3000, "warning"));

            Console.ReadKey();
        }

        static Func<Task> CreatTask(int sleepInterval, string key)
        {
            return () =>
            {
                var counter = 0;
                while (true)
                {
                    counter++;
                    Thread.Sleep(sleepInterval);

                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using var connection = factory.CreateConnection();

                    using var channel = connection.CreateModel();
                    channel.ExchangeDeclare("dev-direct", ExchangeType.Direct);

                    var message = $"Отправлено сообщение типа [{key}] номер {counter}";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "dev-direct", 
                                        routingKey: key, 
                                        basicProperties: null, 
                                        body: body);

                    Console.WriteLine($"Сообщение типа {key} было отправлено [N: {counter}]");
                }
            };
        }
    }
}
