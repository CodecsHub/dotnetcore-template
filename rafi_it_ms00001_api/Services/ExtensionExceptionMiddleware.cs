using Microsoft.AspNetCore.Builder;
using rafi_it_ms00001_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Services
{
    //<summary>
    // @title:  Custom Global Error Trapping
    // @see: <root> startup.cs
    //</summary>
    public static class ExtensionExceptionMiddleware
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
