using ITeam.DotnetCore.Faker;
using ITeam.DotnetCore.FakeServices;
using ITeam.DotnetCore.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using ITeam.DotnetCore.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using ITeam.DotnetCore.Models.Validators;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using ITeam.DotnetCore.WebApi.Handlers;
using Bogus;
using ITeam.DotnetCore.Models;

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
            services.AddScoped<Faker<Product>, ProductFaker>();
            services.AddScoped<Faker<Customer>, CustomerFaker>();

            services.AddScoped<IAuthorizationService, FakeCustomerService>();

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            // services.AddTransient<IValidator<Product>, ProductValidator>();

            // dotnet add package FluentValidation.AspNetCore

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductValidator>())
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
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

            app.UseAuthentication();
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
