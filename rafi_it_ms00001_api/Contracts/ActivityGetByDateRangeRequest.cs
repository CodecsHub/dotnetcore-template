using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Contracts
{
    public class ActivityGetByDateRangeRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
