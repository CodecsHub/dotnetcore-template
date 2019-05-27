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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Models;
using rafi_it_ms00001_api.Repositories;
using rafi_it_ms00001_api.Services;

namespace rafi_it_ms00001_api
{
    public class Startup
    {

        //@referrence: https://exceptionnotfound.net/using-dapper-asynchronously-in-asp-net-core-2-1/
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment environment, IConfiguration configuration, ILoggerFactory loggerFactory)
        {

            _environment = environment;
            _configuration = configuration;
            _loggerFactory = loggerFactory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            //if (_environment.IsDevelopment())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            _configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Custom Extension services
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            //<summary>
            // @title:  Swagger API testing library
            // @description: API utility for auto generate document and test case example
            //              Also base on Nuget Manager swashbuckle.aspnetcore(4.0.1)
            // @see: Extension/ExtensionSartupService.cs (ConfigureSwaggerGen();)
            // @see: Extension/ExtensionActionDescriptor.cs
            // @see: Extension/ExtensionApiVersionOperationFilter.cs
            // @see: <root>/Startup.cs (app.UseSwagger(); and app.UseSwaggerui();)
            // @url:  https://blog.jimismith.me/blogs/api-versioning-in-aspnet-core-with-nice-swagg/
            //</summary>
            services.AddSwaggerGen(ExtensionSwagger.ConfigureSwaggerGen);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddTransient<IV1ActivityRepositories, V1ActivityRepositories>();

            // Global service registration of conne
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ApplicationInformation"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
