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
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace SimpleFrameworkApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Store configuration in static ConfigurationManager
            ConfigurationManager.Configuration = builder.Configuration;
            
            // Add services to the container
            builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); // Add runtime compilation for Razor views
            
            // Configure services from web.config
            builder.Services.AddMvc(options =>
            {
                options.EnableEndpointRouting = true;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "The field is required.");
            })
            .AddNewtonsoftJson();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
    
    public class ConfigurationManager
    {
        public static IConfiguration Configuration { get; set; }
    }
}