using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.ReceivedConsoleClient
{
    // dotnet add package Microsoft.AspNetCore.SignalR.Client

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Receiver!");

            const string url = "http://localhost:5000/signalr/products";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<Product>("AddedProduct", product => Console.WriteLine($"Received {product.Name}"));
            connection.On<string>("Pong", message => Console.WriteLine($"Received {message}"));

            await connection.StartAsync();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
