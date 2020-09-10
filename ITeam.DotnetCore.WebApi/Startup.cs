using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.FakeServices;
using ITeam.DotnetCore.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ITeam.DotnetCore.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using FluentValidation;
using ITeam.DotnetCore.Models;
using ITeam.DotnetCore.Models.Validators;

namespace ITeam.DotnetCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, FakeProductService>();
            services.AddScoped<ProductFaker>();

            // services.AddTransient<IValidator<Product>, ProductValidator>();

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductValidator>())


                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.lop
                //})
                .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/api/customers", context => context.Response.WriteAsync("Hello Customers"));

                endpoints.Map("/dashboard", endpoints.CreateApplicationBuilder()
                    .UseMiddleware<ProductsMiddleware>()
                    .Build());

                endpoints.MapControllers();
            });
        }
    }
}
