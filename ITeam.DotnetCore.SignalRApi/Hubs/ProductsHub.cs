using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.SignalRApi.Hubs
{
    public class ProductsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, "GrupaA");

            return base.OnConnectedAsync();
        }

        public async Task JoinToRoom(string room)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, room);
        }

        public async Task SendAddedProduct(Product product)
        {
            // await this.Clients.All.SendAsync("AddedProduct", product);

            await this.Clients.Others.SendAsync("AddedProduct", product);

            await this.Clients.Group("GrupaA").SendAsync("AddedProduct", product);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.SendAsync("Pong", message);
        }
    }
}
