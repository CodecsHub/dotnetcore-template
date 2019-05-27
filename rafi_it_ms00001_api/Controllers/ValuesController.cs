using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Models;

namespace rafi_it_ms00001_api.Controllers
{
    [Route("api/v{version:Apiversion}/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IV1ActivityRepositories _v1activitiyrepo;

        public ValuesController(IV1ActivityRepositories v1activitiyrepo)
        {
            _v1activitiyrepo = v1activitiyrepo;
        }

        [HttpGet("", Name = "GetAll")]
        //public ActionResult<IEnumerable<string>> Get()
        // @todo: update and finalize the controller request public IActionResult Get() can be substitute
        //the GET using IAction
        //uncomment below to get the Select query with Parameter on it
        //public IActionResult Get([FromBody] V1Branch request)
        public async Task<ActionResult<V1Activity>> Get()
        {
            var output = await _v1activitiyrepo.Get();
            return Ok(output);
        }


        [HttpGet("GetByDate", Name = "GetByDate")]
        //public ActionResult<IEnumerable<string>> Get()
        // @todo: update and finalize the controller request public IActionResult Get() can be substitute
        //the GET using IAction
        //uncomment below to get the Select query with Parameter on it
        //public IActionResult Get([FromBody] V1Branch request)
        public async Task<ActionResult<V1Activity>> Get([FromBody]IIV1ActivityGetByDate model)
        {
            var output = await _v1activitiyrepo.Get(model);
            return Ok(output);
        }


        [HttpGet("GetById", Name = "GetById")]
        //public ActionResult<IEnumerable<string>> Get()
        // @todo: update and finalize the controller request public IActionResult Get() can be substitute
        //the GET using IAction
        //uncomment below to get the Select query with Parameter on it
        //public IActionResult Get([FromBody] V1Branch request)
        public async Task<ActionResult<V1Activity>> Get([FromBody]IIV1ActivityGetById model)
        {
            var output = await _v1activitiyrepo.Get(model);
            return Ok(output);
        }


        [HttpGet("GetBySystemName", Name = "GetBySystemName")]
        //public ActionResult<IEnumerable<string>> Get()
        // @todo: update and finalize the controller request public IActionResult Get() can be substitute
        //the GET using IAction
        //uncomment below to get the Select query with Parameter on it
        //public IActionResult Get([FromBody] V1Branch request)
        public async Task<ActionResult<V1Activity>> Get([FromBody]IIV1ActivityGetBySystemName model)
        {
            var output = await _v1activitiyrepo.Get(model);
            return Ok(output);
        }

        /// <summary>
        /// Returns a group of Employees matching the given first and last names.
        /// </summary>
        /// <remarks>
        /// Here is a sample remarks placeholder.
        /// </remarks>
        /// <param name="firstName">The first name to search for</param>
        /// <param name="lastName">The last name to search for</param>
        /// <returns>A string status</returns>
        [HttpPost("", Name = "Post")]
        //public ActionResult<IEnumerable<string>> Get()
        // @todo: update and finalize the controller request public IActionResult Get() can be substitute
        //the GET using IAction
        //uncomment below to get the Select query with Parameter on it
        //public IActionResult Get([FromBody] V1Branch request)
        public async Task<ActionResult<V1Activity>> Post([FromBody]IIV1ActivityPost model)
        {
            var output = await _v1activitiyrepo.Post(model);
            return Ok(output);
        }

    }
}
