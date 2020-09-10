using ITeam.DotnetCore.Models;
using System;

namespace ITeam.DotnetCore.IServices
{

    public interface IProductService : IEntityService<Product>
    {
        Product Get(string barcode);
    }
   
}
