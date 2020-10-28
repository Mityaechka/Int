using Intemotion.Hubs.Models;
using Intemotion.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Hubs.Extensions
{
    public static class HubExtensions
    {
        public static async Task SendEvent<T>(this IClientProxy client,string name,ServiceResult<T> result)
        {
           await client.SendAsync("event", new HubEvent(name, result.ToObject()));
        }
    }
}
