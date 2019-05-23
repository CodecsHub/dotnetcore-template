using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Contracts
{
    public class ActivityResponse
    {
        public ActivityResponse()
        {
            Data = new List<Activity>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public List<Activity> Data { get; set; }
    }
}
