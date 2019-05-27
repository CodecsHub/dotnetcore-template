using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using rafi_it_ms00001_api.DAO;
using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Repositories
{
    public class V1ActivityRepositories : IV1ActivityRepositories
    {

        private readonly IOptions<UtilityAppSettings> _appSettings;

        // DI name must base on the class name
        public V1ActivityRepositories(IOptions<UtilityAppSettings> appSettings)
        {
            _appSettings = appSettings;
        }


        public async Task<List<V1Activity>> Get()
        {
            using (IDbConnection connection = new SqlConnection(_appSettings.Value.DatabaseConnectionRead))
            {
                string query = "EXEC V1Activity_GetAll";
                var output = await connection.QueryAsync<V1Activity>(query);
                return output.ToList();
            }
        }

        public async Task<List<V1Activity>> Get(IIV1ActivityGetByDate model)
        {
            using (IDbConnection connection = new SqlConnection(_appSettings.Value.DatabaseConnectionRead))
            {
                string query = "EXEC V1Activity_GetDateRange @FromDate, @ToDate";
                var output = await connection.QueryAsync<V1Activity>(query,
                    new {
                        @FromDate = model.FromDate,
                        @ToDate = model.ToDate
                        }
                    );
                return output.ToList();
            }
        }

        public async Task<List<V1Activity>> Get(IIV1ActivityGetById model)
        {
            using (IDbConnection connection = new SqlConnection(_appSettings.Value.DatabaseConnectionRead))
            {
                string query = "EXEC V1Activity_GetId @ActivityId";
                var output = await connection.QueryAsync<V1Activity>(query,
                        new
                        {
                            @ActivityId = model.ActivityId
                        }
                    );
                return output.ToList();
            }
        }

        public async Task<List<V1Activity>> Get(IIV1ActivityGetBySystemName model)
        {
            using (IDbConnection connection = new SqlConnection(_appSettings.Value.DatabaseConnectionRead))
            {
                string query = "EXEC V1Activity_GetSystemName @SystemName";
                var output = await connection.QueryAsync<V1Activity>(query,
                        new
                        {
                            @SystemName = model.SystemName
                        }
                    );
                return output.ToList();
            }
        }

        public async Task<List<V1Activity>> Post(IIV1ActivityPost model)
        {
            using (IDbConnection connection = new SqlConnection(_appSettings.Value.DatabaseConnectionWrite))
            {
                string query = "EXEC V1Activity_Post @SystemName, @ActionName, @UserName, @Remarks, @DateCreated";
                var output = await connection.QueryAsync<V1Activity>(query,
                        new
                        {
                            @SystemName = model.SystemName,
                            @ActionName = model.ActionName,
                            @UserName = model.UserName,
                            @Remarks = model.Remarks,
                            @DateCreated = model.DateCreated
                        }
                    );
                return output.ToList();
            }
        }
    }
}
