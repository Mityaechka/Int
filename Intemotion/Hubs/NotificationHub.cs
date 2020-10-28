using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Hubs
{
    public class NotificationHub : Hub
    {
    }
    public class CameraHub : Hub
    {
        public static List<string> sendAnswers = new List<string>();
        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public async Task NewConnection()
        {
            await Clients.All.SendCoreAsync("NewConnection", new object[] { });
        }
        public async Task SendMyPeer(string peer)
        {
            await Clients.Others.SendCoreAsync("Peer", new object[] { peer, Context.ConnectionId });
        }
        public async Task SendAnswer(string answer, string connectionId)
        {
            await Clients.Client(connectionId).SendCoreAsync("Answer", new object[] { answer, Context.ConnectionId, sendAnswers });
            sendAnswers.Add(connectionId);
        }
    }
}
