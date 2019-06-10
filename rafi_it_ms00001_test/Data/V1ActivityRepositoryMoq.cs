using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rafi_it_ms00001_test.Data
{
    public class V1ActivityRepositoryMoq : IV1ActivityRepositories
    {
        private readonly List<V1Activity> _v1Activity;

        public V1ActivityRepositoryMoq()
        {
            _v1Activity = new List<V1Activity>()
            {
                new V1Activity() { ActivityId = 1, SystemName = "Microservice 00001", ActionName ="Add",
                    UserName ="francisco.abayon@rafi.ph", Remarks = "sample data",DateCreated = new DateTime(2019, 08, 5) },
                new V1Activity() { ActivityId = 2, SystemName = "Microservice 00001", ActionName ="Add",
                    UserName ="francisco.abayon@rafi.ph", Remarks = "sample data",DateCreated = new DateTime(2019, 09, 6) },
                new V1Activity() { ActivityId = 3, SystemName = "Microservice 00002", ActionName ="Edit",
                    UserName ="francisco.abayon@rafi.ph", Remarks = "sample data",DateCreated = new DateTime(2019, 10,6) },
                new V1Activity() { ActivityId = 4, SystemName = "Microservice 00002", ActionName ="Edit",
                    UserName ="lord.jee.canceran@rafi.ph", Remarks = "sample data",DateCreated = new DateTime(2019, 10, 7) },
                new V1Activity() { ActivityId = 5, SystemName = "Microservice 00002", ActionName ="Delete",
                    UserName ="lord.jee.canceran@rafi.ph", Remarks = "sample data",DateCreated = new DateTime(2019, 12, 5) }
            };
        }

        public Task<List<V1Activity>> Get()
        {
            // var output = await connection.QueryAsync<V1Activity>(query);
            throw new NotImplementedException();
        }

        public Task<List<V1Activity>> Get(IIV1ActivityGetByDate model)
        {
            throw new NotImplementedException();
        }

        public Task<List<V1Activity>> Get(IIV1ActivityGetById model)
        {
            throw new NotImplementedException();
        }

        public Task<List<V1Activity>> Get(IIV1ActivityGetBySystemName model)
        {
            throw new NotImplementedException();
        }

        public Task<List<V1Activity>> Post(IIV1ActivityPost model)
        {
            throw new NotImplementedException();
        }
    }
}
