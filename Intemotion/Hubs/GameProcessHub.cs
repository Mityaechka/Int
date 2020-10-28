using Castle.DynamicProxy.Generators;
using Intemotion.Entities;
using Intemotion.Entities.Rounds.FirstRound;
using Intemotion.Entities.Rounds.SecondRound;
using Intemotion.Hubs.Extensions;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.SystemNotifications;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intemotion.Hubs
{

    public class GameProcessHub : BaseHub
    {
        private readonly IGameProcessService gameProcessService;
        public GameProcessHub(IGameProcessService gameProcessService)
        {
            this.gameProcessService = gameProcessService;
        }
        public async Task ConnectUserToGame(int gameProcessId)
        {
            var result = await gameProcessService.TryAddUserToGame(gameProcessId, Context.ConnectionId);
            await Clients.Caller.SendEvent("ConnectUserToGame", result);
        }
        public async Task NewConnection()
        {
            var gameProcess = gameProcessService.GetGameProcess(Context.ConnectionId);
            if (gameProcess.IsSuccess)
            {
                await Clients.Clients(gameProcess.Data.ConnectedUsers
                    .Select(x=>x.ConnectionId)
                    .ToArray()).SendCoreAsync("NewConnection", new object[] { });
            }
        }
        public async Task SendMyPeer(string peer)
        {
            var gameProcess = gameProcessService.GetGameProcess(Context.ConnectionId);
            if (gameProcess.IsSuccess)
            {
                await Clients.Clients(gameProcess.Data.ConnectedUsers
                    .Where(x => x.ConnectionId != Context.ConnectionId)
                    .Select(x => x.ConnectionId)
                    .ToArray()).SendCoreAsync("Peer", new object[] { peer, Context.ConnectionId });
            }
                
        }

        //public async Task GetOffers()
        //{
        //    var gameProcess = gameProcessService.GetGameProcess(Context.ConnectionId);
        //    if (gameProcess.IsSuccess)
        //    {
        //        await Clients.Caller.SendCoreAsync("Offers", new object[]{ gameProcess.Data.ConnectedUsers.Where(x => x.ConnectionId != Context.ConnectionId&&x.WebRTCOffer!=null)
        //                                                                                   .Select(x => new {offer= x.WebRTCOffer, x.ConnectionId }).ToArray() });
        //    }
        //}
        //public async Task NewOffer(string offer)
        //{
        //    var connectedUser = gameProcessService.GetConnectedUser(Context.ConnectionId);
        //    if (connectedUser.IsSuccess)
        //    {
        //        connectedUser.Data.WebRTCOffer = offer;
        //        await gameProcessService.Save();
        //        await Clients.Clients(connectedUser.Data.GameProcess.ConnectedUsers.Select(x => x.ConnectionId).ToArray())
        //            .SendCoreAsync("NewOffer", new object[] { });
        //    }
        //}

        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task GetGameState()
        {
            var result = await gameProcessService.GetGameProcess();
            if (result.IsSuccess)
            {
                await Clients.Caller.SendEvent("GetGameState",
                    new ServiceResult<GameProcessState>
                    {
                        IsSuccess = true,
                        Data = result.Data.State
                    });
            }
            else
            {
                await Clients.Caller.SendEvent("Error", result);
            }
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var result = await gameProcessService.RemoveUserFromGame(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
    public class FirstRoundQuestionViewModel
    {
        public FirstRoundQuestionViewModel(DateTime time, IntellectualQuestion question,int[] cost)
        {
            Answers = question.Answers.Select(x => new { x.Id, x.Value });
            Category = question.QuestionsCategory.Name;
            Question = question.Value;
            Cost = cost[question.Index];
            Time = time;
        }

        public string Category { get; set; }
        public string Question { get; set; }
        public object Answers { get; set; }
        public DateTime Time { get; set; }
        public int Cost { get; set; }
    }
    public class SecondRoundQuestionViewModel
    {
        public SecondRoundQuestionViewModel(DateTime time, TruthQuestion question)
        {
            Question = question.Value;
            Time = time;
        }

        public string Question { get; set; }
        public DateTime Time { get; set; }
    }

}
