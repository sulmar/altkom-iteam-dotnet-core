﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITeam.DotnetCore.Models.Validators
{
    // dotnet add package FluentValidation
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Color).NotEmpty();
            RuleFor(p => p.BarCode).NotEmpty().Length(13);
        }
    }
}
