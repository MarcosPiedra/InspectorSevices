using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;
using FluentValidation.AspNetCore;
using FluentValidation;
using InspectorServices.Configurations;
using InspectorServices.Domain.Models;
using InspectorServices.SqlDataAccess;
using InspectorServices.Domain;
using InspectorServices.WebAPI.DTOs;
using InspectorServices.WebAPI.Validation;

namespace FootballServices
{
    public class Startup
    {
        public readonly IConfiguration configuration;
        private readonly bool isTest = false;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables();

            this.configuration = builder.Build();

            isTest = env.IsEnvironment("Test");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connConfig = configuration.GetSection("ConnectionStrings").Get<ConnectionConfiguration>();
            if (isTest)
            {
                var guid = Guid.NewGuid().ToString();
                File.Copy("Inspector.db", $"{guid}.db");
                connConfig.DatabaseConnection = $"Data Source={guid}.db";
            }

            services.AddMvc()
                    .AddFluentValidation()
                    .AddNewtonsoftJson();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
            services.AddApiVersioning();

            services.AddDbContext<InspectorContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlite(connConfig.DatabaseConnection);
            }, ServiceLifetime.Transient);

            services.AddTransient<IRepository<Inspection>, EFRepository<Inspection>>();
            services.AddTransient<IRepository<Inspector>, EFRepository<Inspector>>();

            services.AddTransient<IInspectionService, InspectionService>();

            services.AddTransient<IValidator<InspectionRequest>, InspectionValidator>();
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    string message = $"{context.Request.Path} {context.Request.QueryString} {context.Request.Method}";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Log(LogLevel.Error, contextFeature.Error, message);

                        await context.Response.WriteAsync(new
                        {
                            context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });

            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}
