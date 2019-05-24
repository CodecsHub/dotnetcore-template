using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Repositories
{
    public class V1ActivityRepositories : IV1ActivityRepositories
    {
        public Task<V1Activity> Get()
        {
            throw new NotImplementedException();
        }

        public Task<V1Activity> Get(IIV1ActivityGetByDate model)
        {
            throw new NotImplementedException();
        }

        public Task<V1Activity> Get(IIV1ActivityGetById model)
        {
            throw new NotImplementedException();
        }

        public Task<V1Activity> Get(IIV1ActivityGetBySystemName model)
        {
            throw new NotImplementedException();
        }

        public Task<V1Activity> Post(IIV1ActivityPost model)
        {
            throw new NotImplementedException();
        }
    }
}
