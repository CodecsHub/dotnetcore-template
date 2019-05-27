using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.DAO
{
    public interface IV1ActivityRepositories
    {
        Task<List<V1Activity>> Get();
        Task<List<V1Activity>> Get(IIV1ActivityGetByDate model);
        Task<List<V1Activity>> Get(IIV1ActivityGetById model);
        Task<List<V1Activity>> Get(IIV1ActivityGetBySystemName model);
        Task<List<V1Activity>> Post(IIV1ActivityPost model);
    }
}
