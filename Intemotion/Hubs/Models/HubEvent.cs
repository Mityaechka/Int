using Intemotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Hubs.Models
{
    public class HubEvent
    {
        public string Event { get; set; }
        public HubResult Result { get; set; }

        public HubEvent(string name, ServiceResult<object> result)
        {
            Event = name;
            Result = new HubResult(result);
        }
    }
    public class HubResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public HubResult(ServiceResult<object> result)
        {
            IsSuccess = result.IsSuccess;
            ErrorMessage = result.ErrorMessage;
            Data = result.Data;
        }
    }
}
