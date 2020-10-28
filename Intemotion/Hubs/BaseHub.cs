using Intemotion.Hubs.Models;
using Intemotion.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intemotion.Hubs
{
    public class BaseHub : Hub
    {
        public HubEvent Event(string name, object result)
        {
            return new HubEvent(name, (ServiceResult<object>)result);
        }
    }

}
