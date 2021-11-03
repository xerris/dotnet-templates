using System.Diagnostics;
using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Xerris.DotNet.Core.Extensions;

namespace Xerris.WebApi1
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
            services.AddControllers();
            
            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xerris.WebApi1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xerris.WebApi1 v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health");

                endpoints.MapGet("/", async context =>
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                    var assemblyName = assembly.GetName().Name;
                    var assemblyVersion = assembly.GetName().Version?.ToString();
                    var fileVersion = fileVersionInfo.FileVersion;
                    var productVersion = fileVersionInfo.ProductVersion;

                    var versionObject = new
                    {
                        assemblyName,
                        assemblyVersion,
                        fileVersion,
                        productVersion
                    };

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(versionObject.ToJson());
                });
            });
        }
    }
}
