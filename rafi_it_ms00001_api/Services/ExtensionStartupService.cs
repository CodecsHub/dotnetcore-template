using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using rafi_it_ms00001_api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Services
{
    public static class ExtensionStartupService
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            //We are using the basic settings for adding CORS policy because for this project allowing any origin, method, and header is quite enough.
            //But you can be more restrictive with those settings if you want.
            //Instead of the AllowAnyOrigin() method which allows requests from any source,
            //you could use the WithOrigins("http://www.something.com") which will allow requests
            //just from the specified source. Also, instead of AllowAnyMethod() that allows all HTTP methods,
            //you can use WithMethods("POST", "GET") that will allow only specified HTTP methods.
            //Furthermore, you can make the same changes for the AllowAnyHeader() method by using,
            //for example, the WithHeaders("accept", "content-type") method to allow only specified headers.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());
            });
        }

        // l help us with the IIS deployment
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }




        // API Versioning library
        public static void ConfigureAPIVersioning(this IServiceCollection services)
        {
            // referrences
            // https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx
            // https://dzone.com/articles/api-versioning-in-net-core
            // https://koukia.ca/api-versioning-in-asp-net-core-2-0-1b55970aa29d
            // https://neelbhatt.com/2018/04/21/api-versioning-in-net-core/
            // https://dotnetcoretutorials.com/2017/01/17/api-versioning-asp-net-core/
            services.AddApiVersioning(optionVersioning =>
            {
                optionVersioning.ReportApiVersions = true;
                optionVersioning.AssumeDefaultVersionWhenUnspecified = true;
                optionVersioning.DefaultApiVersion = new ApiVersion(1, 0);

                //We can allow clients to request a specific API version by media type.
                //This option can be enabled by adding below line in the API versioning options in the ConfigureService method:
                optionVersioning.ApiVersionReader = new MediaTypeApiVersionReader();

                // Uncomment this line to enable only the api version header
                //Once you enable this, Query string approach would not work.
                //optionVersioning.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

                // Doesnt need to redeclare the ApiVersion attribute on each controller name
                optionVersioning.Conventions.Controller<ValuesController>().HasApiVersion(new ApiVersion(1, 0));


                // use [MapToApiVersion("1.0")] after httpget/httppost in the controller for mapping specific method
            });

        }
    }
}
