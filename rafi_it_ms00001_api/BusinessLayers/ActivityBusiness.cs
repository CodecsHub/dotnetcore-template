using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rafi_it_ms00001_api.Contracts;
using rafi_it_ms00001_api.Models;

namespace rafi_it_ms00001_api.BusinessLayers
{
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityBusiness(IActivityRepository acitivtyRepository)
        {
            _activityRepository = acitivtyRepository;
        }

       

        public async Task<ActivityResponse> Get()
        {
            ActivityResponse activityResponse = new ActivityResponse();
            IEnumerable<Activity> acitvity = await _activityRepository.GetAllAsync();

            if (acitvity.ToList().Count == 0)
            {
                activityResponse.Message = "Products not found.";
            }
            else
            {
                activityResponse.Data.Add(acitvity);
            }

            return activityResponse;
        }

        public Task<ActivityResponse> Get(ActivityGetByDateRangeRequest productRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityResponse> Get(ActivityGetByIdRequest productRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityResponse> Get(ActivityGetBySystemNameRequest productReques)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityResponse> Post(ActivityPostRequest productRequest)
        {
            throw new NotImplementedException();
        }
    }
}
