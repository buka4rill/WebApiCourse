using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiPSCourse.Controllers;
using WebApiPSCourse.Data;

namespace WebApiPSCourse
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
            // Register Application Db Context
            services.AddDbContext<CampContext>();

            services.AddScoped<ICampRepository, CampRepository>();

            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add API Versioning
            // Required in ASPNET CORE >=2.2 
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true; // Assume a default version if not specified (1.1)
                opt.DefaultApiVersion = new ApiVersion(1, 1); // Default version is 1,1 (1.1)
                opt.ReportApiVersions = true; // Add headers to responses to say what API versions are gonna be supported by a certain URI

                // Version with headers
                /** 
                opt.ApiVersionReader = new QueryStringApiVersionReader("ver"); // Figure out how to determine the version from some part of the request
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version"); // Key => Value (Header parameter) where key is "X-Version" 
                **/

                // Using Multiple Versioning Methods
                // Calling ApiVersionReader as a static class...
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("ver", "version", "api-version"),
                    new UrlSegmentApiVersionReader()
                );

                // Versioning with URL
                // opt.ApiVersionReader = new UrlSegmentApiVersionReader();

                // Versioning By Conventions
                // opt.Conventions.Controller<TalksController>()
                //     .HasApiVersion(new ApiVersion(1, 0))
                //     .HasApiVersion(new ApiVersion(1, 1))
                //     .Action(c => c.Delete(default(string), default(int)))
                //     .MapToApiVersion(1, 1);

            });

            services.AddControllers();
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
                endpoints.MapControllers();
            });
        }
    }
}
