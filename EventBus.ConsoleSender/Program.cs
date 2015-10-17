using EventBus.Messaging;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.ConsoleSender
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Creating hub");
            using (var hubConnection = new HubConnection("http://localhost:1906/"))
            {
                var eventHubProxy = hubConnection.CreateHubProxy("EventHub");
                eventHubProxy.On<Message>("Receive", message => OnReceive(message));
                //eventHubProxy.On<Message>("UpdateConfiguration", message => OnUpdateConfiguration(message));
                hubConnection.Start().Wait();
                //eventHubProxy.Invoke<Message>("RequestConfiguration").Wait();

                Console.WriteLine("Hub created");
                while (true)
                {
                    Console.Write("Values: ");
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Console.ReadLine());
                    var message = new Message
                    {
                        Sender = typeof(Program).FullName,
                        Target = "ArtNet",
                        Time = DateTime.Now,
                        Values = values
                    };
                    Console.WriteLine("Sending message");
                    eventHubProxy.Invoke<Message>("Send", message).Wait();
                    Console.WriteLine("Message sent");
                }
            }
        }

        static void OnUpdateConfiguration(Message message)
        {
            Console.WriteLine("ConfigUpdate:");
            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
        }

        static void OnReceive(Message message)
        {
            Console.WriteLine("Received:");
            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
        }
    }
}
