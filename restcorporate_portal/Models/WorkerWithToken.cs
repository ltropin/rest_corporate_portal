using System;
using restcorporate_portal.ResponseModels;

namespace restcorporate_portal.Models
{
    public class WorkerWithToken
    {
        public ResponseWorkerList Worker { get; set; }
        public string Token { get; set; }
    }
}
