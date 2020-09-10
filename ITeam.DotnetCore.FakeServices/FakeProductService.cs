using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ITeam.DotnetCore.FakeServices
{

    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public Product Get(string barcode)
        {
            return entities.SingleOrDefault(p => p.BarCode == barcode);
        }

        public override void Remove(int id)
        {
            Product product = Get(id);
            product.IsRemoved = true;
        }
    }
}
