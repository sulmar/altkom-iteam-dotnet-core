using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.SenderConsoleClient
{

    // dotnet add package Microsoft.AspNetCore.SignalR.Client

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Sender!");

            const string url = "http://localhost:5000/signalr/products";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>("Pong", message => Console.WriteLine($"Received {message}"));

            await connection.StartAsync();

            await connection.SendAsync("Ping", "Hello");

            ProductFaker productFaker = new ProductFaker();

            // Product product = productFaker.Generate();

            var products = productFaker.GenerateForever();

            foreach (var product in products)
            {
                await connection.SendAsync("SendAddedProduct", product);

                Console.WriteLine($"Sent {product.Name}");

                await Task.Delay(TimeSpan.FromSeconds(0.1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
