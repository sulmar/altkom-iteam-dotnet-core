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
using ITeam.DotnetCore.WebApi.Requirements;
using Microsoft.AspNetCore.Authorization;
using ITeam.DotnetCore.WebApi.HealthCheckers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Options;
using System.Threading;
using System;
using Microsoft.AspNetCore.ResponseCompression;

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

            services.AddScoped<IServices.IAuthorizationService, FakeCustomerService>();

            // var options = Options.Create(new FakeProductServiceOptions { Count = 10 });
            // Configuration.GetSection("Products").Bind(options);

            services.Configure<FakeProductServiceOptions>(Configuration.GetSection("Products"));

            // Action<FakeProductServiceOptions> options = o => Options.Create(new FakeProductServiceOptions { Count = 10 });

            //Action<FakeProductServiceOptions> options = o => 
            //{
            //    o.Count = 10;
            //};

            // services.Configure(options);

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AtLeast18", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(18)));

                options.AddPolicy("AtLeast7", policy =>
                   policy.Requirements.Add(new MinimumAgeRequirement(7)));
            });

              services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();


            services.AddHealthChecks()
                .AddCheck<RandomHealthCheck>("random");

            // dotnet add package Microsoft.AspNetCore.ResponseCompression
            services.Configure<GzipCompressionProviderOptions>(options => 
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });


            // dotnet add package AspNetCore.HealthChecks.UI

            //   services.AddHealthChecksUI();

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

            app.UseStaticFiles();

            app.UseResponseCompression();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            string smsApiUrl = Configuration["SmsApiUrl"];

            string smsApiLimit = Configuration["SmsApi:Limit"];


            string googleSecretKey = Configuration["google-secret-key"];

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/api/customers", context => context.Response.WriteAsync("Hello Customers"));

                endpoints.Map("/dashboard", endpoints.CreateApplicationBuilder()
                    .UseMiddleware<ProductsMiddleware>()
                    .Build());


                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
                );

              // endpoints.MapHealthChecksUI();

                endpoints.MapControllers();
            });
        }
    }
}
