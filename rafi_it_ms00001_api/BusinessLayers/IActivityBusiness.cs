using rafi_it_ms00001_api.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.BusinessLayers
{
    interface IActivityBusiness
    {
        Task<ActivityResponse> Get();
        Task<ActivityResponse> Get(ActivityGetByDateRangeRequest productRequest);
        Task<ActivityResponse> Get(ActivityGetByIdRequest productRequest);
        Task<ActivityResponse> Get(ActivityGetBySystemNameRequest productReques);
        Task<ActivityResponse> Post(ActivityPostRequest productRequest);
    }
}
