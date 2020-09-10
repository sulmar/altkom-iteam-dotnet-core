﻿using Bogus;
using ITeam.DotnetCore.Models;
using System;

namespace ITeam.DotnetCore.Faker
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
            RuleFor(p => p.CreatedDate, f => f.Date.Past(4));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
        }
    }
}
