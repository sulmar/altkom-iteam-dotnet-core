using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITeam.DotnetCore.WebApi.Middlewares
{
    public class ProductsMiddleware
    {
        private readonly RequestDelegate next;

        public ProductsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {            
            await context.Response.WriteAsync("Products");
        }
    }
}
