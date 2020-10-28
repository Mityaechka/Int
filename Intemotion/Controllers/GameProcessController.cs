using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intemotion.Configs;
using Intemotion.Entities;
using Intemotion.Hubs;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.SystemNotifications;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Intemotion.Controllers
{
    [Route("api/gameProcess")]
    [ApiController]
    public class GameProcessController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IGameService gameService;
        private readonly IGameProcessService gameProcessService;
        private readonly GameProcessContext gameProcessContext;
        private readonly SystemNotification systemNotification;
        private readonly IOptions<RoundConfig> roundOptions;

        public GameProcessController(IAuthService authService, IGameService gameService,
                                     IGameProcessService gameProcessService, GameProcessContext gameProcessContext,
                                     SystemNotification systemNotification,IOptions<RoundConfig> roundOptions)
        {
            this.authService = authService;
            this.gameService = gameService;
            this.gameProcessService = gameProcessService;
            this.gameProcessContext = gameProcessContext;
            this.systemNotification = systemNotification;
            this.roundOptions = roundOptions;
        }
        [Route("connectionId")]
        [HttpGet]
        public async Task<ControllerResult<string>> GetConnectionId()
        {
            return Result((await gameProcessService.GetConnectedUser()).Convert(x => x.ConnectionId));
        }
        [Route("join")]
        public async Task<ObjectControllerResult> JoinToGame(string nickname = null)
        {
            var result = await gameProcessService.JoinToGameProcess(nickname);
            await gameProcessService.ResetGame();
            return Result(result);
        }
        [Route("")]
        public async Task<ControllerResult<GameProcessViewModel>> GetGameProcess()
        {
            var result = await gameProcessService.GetGameProcess();
            return Result(result.Convert(x => new GameProcessViewModel(x)));
        }
        [Route("statistic")]
        public async Task<ObjectControllerResult> Statistic()
        {
            var result = await gameProcessService.GetStatistic();
            return Result(result);
        }
        [Route("rounds/first/table")]
        public async Task<ControllerResult<object>> GetFirstRoundTable()
        {
            var result = await gameProcessService.GetGameProcess();
            return Result(result.Convert(x => (object)new
            {
                Categories = x.Game.FirstRound.QuestionsCategories.Select(u => new { u.Name, u.Id }),
                Cost = roundOptions.Value.FirstRoundQuestionCost,
                FirstRound = x.FirstRoundResult.MixedQuestions.Take(x.FirstRoundResult.QuestionIndex).Select(u => new { CategoryId = u.QuestionsCategory.Id, Index = u.Index })
            }));
        }
        [Route("chat/messages")]
        [HttpPost]
        public async Task<ControllerResult<int>> SendChatMessage(string message)
        {
            var result = await gameProcessService.SendUserChatMessage(message);
            if (result.IsSuccess)
            {
               // systemNotification.CallEvent("SendMessage",result.Data);
            }
            return Result(result);
        }
        [Route("rounds/first/wait")]
        public async Task<ObjectControllerResult> StartWaitingFirstRound()
        {
            var result = await gameProcessService.StartWaitingFirstRound();
            if (result.IsSuccess)
            {
                await gameProcessContext.SendGameState();
            }
            return Result(result);
        }
        [Route("rounds/first/start")]
        public async Task<ObjectControllerResult> StartFirstRound()
        {
            var result = await gameProcessService.StartFirstRound();
            if (result.IsSuccess)
            {
                await gameProcessContext.SendGameState();
                await gameProcessContext.PlayFirstRound();
            }
            return Result(result);
        }
        [HttpGet]
        [Route("rounds/first/answer")]
        public async Task<ObjectControllerResult> FirstRoundAnswer(int id)
        {
            return Result(await gameProcessService.FirstRoundReceiveAnswer(id));
        }
        [Route("rounds/second/start")]
        public async Task<ObjectControllerResult> StartSecondRound()
        {
            var result = await gameProcessService.StartSecondRound();
            if (result.IsSuccess)
            {
                await gameProcessContext.SendGameState();
                await gameProcessContext.PlaySecondRound();
            }
            return Result(result);
        }
        [HttpGet]
        [Route("rounds/second/answer")]
        public async Task<ObjectControllerResult> SecondRoundAnswer(bool isTruth)
        {
            return Result(await gameProcessService.SecondRoundReceiveAnswer(isTruth));
        }
        [HttpGet]
        [Route("rounds/second/questions-count")]
        public async Task<ControllerResult<int>> GetSecondRoundQuestionsCount()
        {
            return Result(await gameProcessService.GetSecondRoundQuestionsCount());
        }
        [Route("rounds/third/wait")]
        public async Task<ObjectControllerResult> StartWaitingThirdRound()
        {
            var result = await gameProcessService.StartWaitingThirdRound();
            if (result.IsSuccess)
            {
                await gameProcessContext.SendGameState();
            }
            return Result(result);
        }




        [Route("rounds/third/start")]
        public async Task<ObjectControllerResult> StartThirdRound()
        {
            var result = await gameProcessService.StartThirdRound();
            if (result.IsSuccess)
            {
                await gameProcessContext.SendGameState();
                await gameProcessContext.PlayThirdRound();
            }
            return Result(result);
        }

        [HttpPost]
        [Route("rounds/third/chronology/answer")]
        public async Task<ObjectControllerResult> ThirdRoundChrologyAnswer(List<int> answers)
        {
            return Result(await gameProcessService.ThirdRoundChronologyReceiveAnswer(answers));
        }
        [HttpGet]
        [Route("rounds/third/chronology/answer")]
        public async Task<ObjectControllerResult> ThirdRoundMelodyAnswer(int answer)
        {
            return Result(await gameProcessService.ThirdRoundMelodyReceiveAnswer(answer));
        }
        [HttpGet]
        [Route("rounds/third/association/answer")]
        public async Task<ObjectControllerResult> ThirdRoundAssociationReceiveAnswer(int answer)
        {
            return Result(await gameProcessService.ThirdRoundAssociationReceiveAnswer(answer));
        }
    }
    public class GameProcessViewModel
    {
        public GameProcessViewModel(GameProcess model)
        {
            Id = model.Id;
            CreateDate = model.CreateDate;
            StartTime = model.StartTime;
            State = model.State;
            GameId = model.GameId;
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartTime { get; set; }

        public GameProcessState State { get; set; }
        public int GameId { get; set; }
    }
}