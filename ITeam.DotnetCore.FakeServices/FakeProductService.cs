using Bogus;
using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.SearchCriterias;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace ITeam.DotnetCore.FakeServices
{

    public class FakeProductServiceOptions
    {
        public int Count { get; set; }
        public string Url { get; set; }
    }

    public class FakeProductService : FakeEntityService<Product>, IProductService
    {

        // dotnet add package Microsoft.Extensions.Options
        public FakeProductService(Faker<Product> productFaker, IOptions<FakeProductServiceOptions> options)
        {
            entities = productFaker.Generate(options.Value.Count);
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
