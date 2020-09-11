using Bogus;
using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace ITeam.DotnetCore.FakeServices
{

    public class FakeProductService : FakeEntityService<Product>, IProductService
    {
        public FakeProductService(Faker<Product> productFaker)
        {
            entities = productFaker.Generate(100);
        }

        public Product Get(string barcode)
        {
            return entities.SingleOrDefault(p => p.BarCode == barcode);
        }

        public ICollection<Product> Get(ProductSearchCriteria searchCriteria)
        {
            IQueryable<Product> query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.Name))
            {
                query = query.Where(p => p.Name.StartsWith(searchCriteria.Name));
            }

            if (!string.IsNullOrEmpty(searchCriteria.Color))
            {
                query = query.Where(p => p.Color == searchCriteria.Color);
            }

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(p => p.UnitPrice >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(p => p.UnitPrice <= searchCriteria.To);
            }

            return query.ToList();
        }

        public override void Remove(int id)
        {
            Product product = Get(id);
            product.IsRemoved = true;
        }
    }
}
