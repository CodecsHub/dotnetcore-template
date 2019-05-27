using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Helpers;
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

            //<summary>
            // @title: API Versioning
            // @description: Usage for api to the controller with API Versioning pattern
            //              Also base on Nuget Manager microsoftaspnetcore.mvc.versioning(2.2.0)
            //              Also base on Nuget Manager microsoftaspnetcore.mvc.versioning.apiexplorer(2.2.0)
            // @see: Extension/ExtensionSartupService.cs (ConfigureAPIVersioning();)
            // @see: <root>/Startup.cs (app.UseSwagger and app.UseSwaggerui)
            //</summary>
            services.ConfigureAPIVersioning();


            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;

                // basd on tutorial can be used but in the intelense it will deprecated
                //config.InputFormatters.Add(new XmlSerializerInputFormatter());
                //config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                // config.OutputFormatters.Add(new JsonInputFormatter());
                //@todo: extend the content negation
                //@referrence: https://code-maze.com/content-negotiation-dotnet-core/
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvcCore().AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true);

            // Add register to centrilize action filter validation
            services.AddScoped<ValidationFilterAttribute>();

            // start of custom DI in startup
            // @url: https://weblog.west-wind.com/posts/2016/May/23/Strongly-Typed-Configuration-Settings-in-ASPNET-Core
            // @see: <root>/startup.cs
            // @see: Controller/HomeController.cs
            services.AddOptions();



            services.AddTransient<IV1ActivityRepositories, V1ActivityRepositories>();

            // Global service registration of conne
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<UtilityAppSettings>(_configuration.GetSection("ApplicationInformation"));
            services.Configure<UtilityAppSettings>(_configuration.GetSection("WebToken"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            //<summary>
            // @title:  Custom Global Error Trapping
            // @description: Custom error helper class base on http request error try/catch statement
            //              also to globalize the location of Try/Catch statement with clean controller
            // @see: Servces/ExtensionExceptionMiddleware.cs
            // @see: Herlper/ExceptionMiddleware.cs
            // @see: Model/UtitlityErrorDetails.cs
            // @url: https://jack-vanlightly.com/blog/2017/8/23/api-series-part-2-swagger
            //</summary>
            app.ConfigureCustomExceptionMiddleware();

            //<summary>
            // @title:  Swagger
            // @description: Instantiate the swagger(the application)
            //              also instantiate the swaggerui (the view and layout of swagger with the corresponding API)
            // @see: <root>/Startup.cs (services.ConfigureSwaggerGen();)
            // @todo: dynamic swagger.json file base on appsettings.json with IConfigure
            // @todo: loop the swagger versioning by simple increment
            //</summary>
            app.UseSwagger(ExtensionSwagger.ConfigureSwagger);
            app.UseSwaggerUI(ExtensionSwagger.ConfigureSwaggerUI);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //@url ; https://www.youtube.com/watch?v=Y5ZLhxZtww8&t=459s
            // Configure the HTTP request pipeline.
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseMiddleware<MiddlewareTokenManager>();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None
            });

                // @title: secure the web http headers
            // @todo: refactor to extension services
            // Begin
            //X-Content-Type-Option Header
            app.UseXContentTypeOptions();

            // Referrer-Policy Header
            app.UseReferrerPolicy(opts => opts.NoReferrer());

            //X-XSS-Protection Header
            app.UseXXssProtection(opts => opts.EnabledWithBlockMode());

            //X-Frame-Option Header
            app.UseXfo(opts => opts.Deny());

            //Content-Security-Header Policy
            app.UseCsp(opts => opts
            .BlockAllMixedContent()
            .StyleSources(s => s.Self())
            .StyleSources(s => s.UnsafeInline())
            .FontSources(s => s.Self())
            .FormActions(s => s.Self())
            .FrameAncestors(s => s.Self())
            .ImageSources(s => s.Self())
            .ScriptSources(s => s.Self())
            );
            //End

            app.UseMvc();
        }
    }
}
