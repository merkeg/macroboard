using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using ASP.NETCoreWebApplication;
using Macroboard.Driver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Macroboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDeviceDriver, DeviceDriver>();
            services.AddControllersWithViews();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddSwaggerGen(c =>
            {
                
                var xmlCommentPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlCommentPath);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Macroboard",
                    Version = "v1",
                    Description = "An api to get the information about the macroboard",
                    Contact = new OpenApiContact()
                    {
                        Name = "Egor Merk",
                        Email = "contact@merkeg.de",
                        Url = new Uri("https://twitter.com/merkegor")
                    }
                });
            });
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "frontend/build"; });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseSwagger();
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Macroboard API";
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.RoutePrefix = "api/docs";
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "frontend";

                if (env.IsDevelopment())
                {
                    spa.UseVueDevelopmentServer("serve");

                }
            });
        }
    }
}