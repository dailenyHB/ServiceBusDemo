using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace ServiceBusDemo
{
    class Program
    {
        const string ServiceBusConnectionString = "<service bus connection string>";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {
            ServiceBusConnectionStringBuilder sbconnection = new ServiceBusConnectionStringBuilder(ServiceBusConnectionString);
            queueClient = new QueueClient(sbconnection);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            string message = File.ReadAllText("Messages\\0.json");

            // Send message
            await SendMessagesAsync(message);

            Console.ReadKey();

            await queueClient.CloseAsync();

            Console.WriteLine("======================================================");
            Console.WriteLine("Message sent!!!");
            Console.WriteLine("======================================================");
        }

        static async Task SendMessagesAsync(string messageBody)
        {
            try
            {
                // Create a new message to send to the queue.                
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                Console.WriteLine($"Sending message: {messageBody}");

                // Send the message to the queue.
                await queueClient.SendAsync(message);

            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
