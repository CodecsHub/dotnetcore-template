using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.DAO
{
    interface IV1Activity
    {
        Task<V1Activity> Get();
        Task<V1Activity> Get(IIV1ActivityGetByDate model);
        Task<V1Activity> Get(IIV1ActivityGetById model);
        Task<V1Activity> Get(IIV1ActivityGetBySystemName model);
        Task<V1Activity> Post(IIV1ActivityPost model);
    }
}
