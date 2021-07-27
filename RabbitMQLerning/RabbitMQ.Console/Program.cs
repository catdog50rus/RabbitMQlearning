using RabbitMQLearning.Producer;
using RabbitQMLerning.Consumer;
using System;

namespace RabbitMQ.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ВВедите сообщение");
                Console.WriteLine();
                
                var sendMessage = Console.ReadLine();

                var producer = new RabbitProducer();

                var result = producer.SendMessage(sendMessage);

                Console.WriteLine($"Результат отправки сообщения: {result}");

                Console.ReadKey();
            }
            
        }
    }
}
