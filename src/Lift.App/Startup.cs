using Lift.App.Helpers;
using Lift.App.Helpers.Standard;
using Lift.App.Middleware;
using Lift.Domain.IRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sentry.AspNetCore;
using System;
using System.IO;
using System.Reflection;

namespace Lift.App {
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBuilding buildingRepo)
        {

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lift.App v1"));

            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseSentryTracing();
            app.UseHealthChecks("/hc");

            buildingRepo.SetDefaultConfig();

            app.Run(async (context) =>
            {
                var envString = env.IsProduction() ? "PROD" : "DEV";
                await context.Response.WriteAsync("LiftManagament Backend " + envString);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureInfrustructure(this.Configuration);
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            services.ConfigureLogger();
            services.AddOptionsStandardExtensions();
            services.AddWebServices();

            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Lift.App", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddLogging(a =>
            {
                a.AddConsole();
                a.AddSentry();
            });

            services.AddHealthChecks();
        }
    }
}
