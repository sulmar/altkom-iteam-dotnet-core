using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ITeam.DotnetCore.FakeServices
{

    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public FakeProductService(ProductFaker productFaker)
        {
            entities = productFaker.Generate(100);
        }

        public Product Get(string barcode)
        {
            return entities.SingleOrDefault(p => p.BarCode == barcode);
        }

        public ICollection<Product> Get(ProductSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int id)
        {
            Product product = Get(id);
            product.IsRemoved = true;
        }
    }
}
