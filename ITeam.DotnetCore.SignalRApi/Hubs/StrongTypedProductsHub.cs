using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.SignalRApi.Hubs
{
    public class StrongTypedProductsHub : Hub<IProductClient>
    {
        public async Task SendAddedProduct(Product product)
        {
            await this.Clients.Others.AddedProduct(product);
        }

        public async Task Ping(string message)
        {
            await Clients.Caller.Pong(message);
        }
    }
}
