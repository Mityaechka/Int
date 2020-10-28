using Intemotion.Hubs;
using Intemotion.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.SystemNotifications
{
    public class SystemNotification
    {
        private readonly IServiceProvider serviceProvider;

        public List<SystemEvent> Events { get; set; } = new List<SystemEvent>();
        public SystemNotification(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void RegisterEvent(SystemEvent systemEvent)
        {
            Events.Add(systemEvent);
        }
        public void CallEvent(string name, object data)
        {

            Events.Where(x => x.Name == name).ToList()?.ForEach(x => x.Action(serviceProvider.CreateScope().ServiceProvider, data));
        }
    }
    public class SystemEvent
    {
        public Action<IServiceProvider, object> Action { get; set; }
        public string Name { get; set; }
    }
}
