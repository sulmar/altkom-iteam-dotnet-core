using ITeam.DotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.IServices
{
    public interface IProductClient
    {
        Task AddedProduct(Product product);
        Task Pong(string message);
    }
}
