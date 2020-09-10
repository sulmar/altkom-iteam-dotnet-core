using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ITeam.DotnetCore.Api.Middlewares;
using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.FakeServices;
using ITeam.DotnetCore.IServices;
using ITeam.DotnetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITeam.DotnetCore.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, FakeProductService>();
            services.AddScoped<ProductFaker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Logger (middleware)
            //app.Use(async (context, next) =>
            //{
            //    Trace.WriteLine($"{context.Request.Method} {context.Request.Path}");

            //    await next();

            //    Trace.WriteLine($"{context.Response.StatusCode}");
            //});


            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = 404;
            //        await context.Response.WriteAsync("Brak dostepu");
            //    }
            //});


            // app.UseMiddleware<LoggerMiddleware>();
            // app.UseMiddleware<AuthorizationMiddleware>();

            app.UseLogger();
            app.UseCustomAuthorization();

            // api/products

            //  app.Map("/api/products", context => context.UseMiddleware<ProductsMiddleware>());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("api/products/{id:int}", async context =>
                {
                    IProductService productService = context.RequestServices.GetRequiredService<IProductService>();

                    int id =  Convert.ToInt32(context.Request.RouteValues["id"]);

                    Product product = productService.Get(id);

                    context.Response.Headers.Add("Content-Type", "application/json");
                    await JsonSerializer.SerializeAsync(context.Response.Body, product);

                   // await context.Response.WriteAsync($"Hello Product {id}");
                });

                endpoints.Map("/api/products", context => context.Response.WriteAsync("Hello Products"));

                

                
            });

            app.Run(context => context.Response.WriteAsync("Hello World!"));
        }
    }
}
