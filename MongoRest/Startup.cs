using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoRest.Models;

namespace MongoRest
{
    public class Startup
    {
        public IConfiguration Config { get; }

        // >> Use env vars, commandline args and config services
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger services
            services.AddSwaggerDocument();

            services.AddControllers();
            services.AddLogging(loggers =>
            {
                loggers.AddConsole();
                loggers.AddDebug();
            });
            services.AddSingleton<IAccountCrud, AccountMock>();
            // services.AddSingleton<IAccountCrud, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // >> Add logging provider
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            // >> Use env vars, commandline args and config services
            if (env.IsDevelopment())
            {
                logger.LogInformation("IsDevelopment={0}, USER={1}",new object[] { env.IsDevelopment(), Config["USER"]});
                app.UseDeveloperExceptionPage();
            }

            // >> Use command line args
            // dotnet watch run-- LOG_STATUS_CODE = false -> override setting file vars
            bool logStatusCode = bool.TryParse(Config["LOG_STATUS_CODE"], out logStatusCode);

            // >> middleware example [looger]
            app.Use(async (context, next) =>
            {
                var req = context.Request;
                logger.LogInformation($"got request...{Environment.NewLine}Path: {req.Path}{Environment.NewLine}Method: {req.Method}{Environment.NewLine}QueryString: {req.QueryString}");

                await next();

                if (logStatusCode)
                {
                    if (context.Response.StatusCode == 200)
                    {
                        logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} response statusCode: {context.Response.StatusCode}");
                    }
                    else
                    {
                        logger.LogCritical($"{DateTime.UtcNow.ToLongTimeString()} response statusCode: {context.Response.StatusCode}");
                    }
                }
            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                // OR + ?
                endpoints.MapControllers();
                // use another route middleware to response with a message or redirect to another location
                endpoints.MapGet("/", async context =>
                {
                    // await context.Response.WriteAsync("Hello World!");
                    // logger.LogInformation($"{app.Properties}");

                    context.Response.Redirect("/api/home");
                });
            });
        }
    }
}
