using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore_api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnetcore_api
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment environment,
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _environment = environment;
            _configuration = configuration;
            _loggerFactory = loggerFactory;

            // @link: https://www.strathweb.com/2017/06/resolving-asp-net-core-startup-class-from-the-di-container/
            // @link: https://weblog.west-wind.com/posts/2016/May/23/Strongly-Typed-Configuration-Settings-in-ASPNET-Core
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // @description: control the dependecy in the logger or else it will slow down the application
            // @todo: refactor the loggin services
            // @url: https://weblog.west-wind.com/posts/2018/Dec/31/Dont-let-ASPNET-Core-Default-Console-Logging-Slow-your-App-down
            services.AddLogging(config =>
            {
                // clear out default configuration
                config.ClearProviders();

                config.AddConfiguration(_configuration.GetSection("Logging"));
                config.AddDebug();
                config.AddEventSourceLogger();
                // @fix database issue on azure https://stackoverflow.com/questions/52050167/how-to-override-local-connection-string-with-azure-connection-string
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == EnvironmentName.Development)
                {
                    config.AddConsole();
                }
            });

            //services.AddHealthChecks(); // Registers health checks services
            //services.ConfigureCors();
            //services.ConfigureIISIntegration();


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
            //services.AddSwaggerGen(ExtensionSwagger.ConfigureSwaggerGen);

            //<summary>
            // @title: API Versioning
            // @description: Usage for api to the controller with API Versioning pattern
            //              Also base on Nuget Manager microsoftaspnetcore.mvc.versioning(2.2.0)
            //              Also base on Nuget Manager microsoftaspnetcore.mvc.versioning.apiexplorer(2.2.0)
            // @see: Extension/ExtensionSartupService.cs (ConfigureAPIVersioning();)
            // @see: <root>/Startup.cs (app.UseSwagger and app.UseSwaggerui)
            //</summary>
            //services.ConfigureAPIVersioning();


            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;

                // basd on tutorial can be used but in the intelense it will deprecated
                //config.InputFormatters.Add(new XmlSerializerInputFormatter());
                //config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                // config.OutputFormatters.Add(new JsonInputFormatter());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvcCore().AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true);



            // start of custom DI in startup
            // @url: https://weblog.west-wind.com/posts/2016/May/23/Strongly-Typed-Configuration-Settings-in-ASPNET-Core
            // @see: <root>/startup.cs
            // @see: Controller/HomeController.cs
            services.AddOptions();

            // base appsettings.json file, indepenedent
            // referrence
            // https://www.c-sharpcorner.com/article/reading-values-from-appsettings-json-in-asp-net-core/
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ApplicationInformation"));
            services.Configure<UtilityAppSettings>(_configuration.GetSection("WebToken"));


            services.AddSingleton<IConfiguration>(_configuration); // IConfiguration explici
                                                                   //end of Custom DI in startup.cs


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
