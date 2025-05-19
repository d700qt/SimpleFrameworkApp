
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

    namespace SimpleFrameworkApp
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
                
                // Store configuration in static ConfigurationManager
                ConfigurationManager.Configuration = builder.Configuration;
                
                // Add services to the container (formerly ConfigureServices)
                builder.Services.AddControllersWithViews();

                // Configure services from web.config
                builder.Services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = true;
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "The field is required.");
                })
                .AddNewtonsoftJson();

                // Add ClientValidationEnabled setting from web.config
                builder.Services.Configure<MvcOptions>(options =>
                {
                    // ClientValidationEnabled was true in web.config
                });

                var app = builder.Build();
                
                // Configure the HTTP request pipeline (formerly Configure method)
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                
                //Added Middleware
                
                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();
                
                // Register areas
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapAreaControllerRoute(
                        name: "areas",
                        areaName: "{area}",
                        pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

                // Register bundles
                // Note: In ASP.NET Core, bundling is handled differently.
// Consider using built-in tag helpers or a library like WebOptimizer
                
                app.Run();
            }
        }
        
        public class ConfigurationManager
        {
            public static IConfiguration Configuration { get; set; }
        }
    }