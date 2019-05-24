using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rafi_it_ms00001_api.BusinessLayers;
using rafi_it_ms00001_api.Contracts;

namespace rafi_it_ms00001_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IActivityBusiness _activityBusiness;

        public ValuesController(IActivityBusiness activityBusiness)
        {
            _activityBusiness = activityBusiness;
        }


        //[ProducesResponseType(200)]
        [HttpGet("", Name = "GetAll")]
        public async Task<ActivityResponse> Get()
        {
            return await _activityBusiness.Get();
        }

        //[ProducesResponseType(200)]
        [HttpGet("GetByDate", Name = "GetByDate")]
        public async Task<ActivityResponse> Get([FromBody]ActivityGetByDateRangeRequest productRequest)
        {
            return await _activityBusiness.Get(productRequest);
        }

        //[ProducesResponseType(200)]
        [HttpGet("GetById", Name = "GetById")]
        public async Task<ActivityResponse> Get([FromBody]ActivityGetByIdRequest productRequest)
        {
            return await _activityBusiness.Get(productRequest);
        }

        //[ProducesResponseType(200)]
        [HttpGet("GetBySystemName", Name = "GetBySystemName")]
        public async Task<ActivityResponse> Get([FromBody]ActivityGetBySystemNameRequest productRequest)
        {
            return await _activityBusiness.Get(productRequest);
        }


        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActivityResponse> Post([FromBody]ActivityPostRequest productRequest)
        {
            return await _activityBusiness.Post(productRequest);
        }



    }
}
