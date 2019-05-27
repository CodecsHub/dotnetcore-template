using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using rafi_it_ms00001_api.Models;

namespace rafi_it_ms00001_api.Controllers
{
    [ApiVersionNeutral]
    [Route("")]
    [ApiController]
    // @todo: document changes based on development
    // @todo: custom the home page
    // @todo: API approach in return app information data
    public class HomeController : ControllerBase
    {
        private UtilityAppSettings _AppSettings { get; set; }

        public HomeController(IOptions<UtilityAppSettings> settings)
        {
            _AppSettings = settings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _AppSettings.ApplicationName; ;
            return Ok(data);
        }
    }
}