using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators;
using Intemotion.Entities;
using Intemotion.Enums;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intemotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
            
        }
        [HttpGet]
        [Route("dates")]
        public async Task<ControllerResult<List<DateTime>>> GetGamesDates()
        {
            return Result((await gameService.GetFreeGames()).Convert(x => x.Select(u => u.PlaneStartDate).ToList()));
        }
        [HttpGet]
        [Route("state/{state}")]
        public ControllerResult<List<GameViewModel>> GaetGamesByState(GameState state) {
            return Result(gameService.GetGamesByState(state).Convert(x => x.Select(u => new GameViewModel(u)).ToList()));
        }
        [HttpGet]
        [Route("state/{state}/{id}")]
        public ControllerResult<GameViewModel> GaetGamesByState(GameState state,int id)
        {
            return Result(gameService.GetGameByState(id,state).Convert(x => new GameViewModel(x)));
        }
        [HttpGet]
        [Route("role/{role}")]
        public async Task<ControllerResult<List<GameViewModel>>> GaetGamesByRole(PlayerRole role)
        {
            return Result((await gameService.GetGamesByPlayerRoles(role)).Convert(x => x.Select(u => new GameViewModel(u)).ToList()));
        }
        [HttpGet]
        [Route("role/{role}/{id}")]
        public async Task<ControllerResult<GameInfoViewModel>> GaetGamesByRole(int id,PlayerRole role)
        {
            return Result((await gameService.GetGameByPlayerRoles(id, role)).Convert(x => new GameInfoViewModel(x)));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ObjectControllerResult> CreateRoorm(GameEditModel model)
        {
            return Result(await gameService.CreateGame(model));
        }
        [HttpGet]
        [Route("{id}/state/recordingPlayers")]
        public async Task<ObjectControllerResult> ChangeGameStateRecordingPlayers(int id)
        {
            return Result(await gameService.ChangeGameStateToRecording(id));
        }
        [HttpGet]
        [Route("{id}/vouchers")]
        public async Task<ControllerResult<List<VoucherViewModel>>> GetGameVouchers(int id)
        {
            return Result((await gameService.GetVouchers(id)).Convert(x=>x.Select(u=>new VoucherViewModel(u)).ToList()));
        }
        [HttpGet]
        [Route("{id}/vouchers/add")]
        public async Task<ObjectControllerResult> AddVoucher(int id)
        {
            return Result(await gameService.AddVoucher(id));
        }

        [HttpGet]
        [Route("join/voucher")]
        public async Task<ObjectControllerResult> JoinToGameWithVoucher(DateTime date,string code)
        {
            return Result(await gameService.JoinToGameWithVoucher(date, code));
        }
        [HttpGet]
        [Route("{id}/bindModeratorToGame")]
        public async Task<ObjectControllerResult> BindModeratorToGame(int id, string moderatorId)
        {
            return Result(await gameService.BindModeratorToGame(id, moderatorId));
        }
        [HttpGet]
        [Route("")]
        public async Task<ControllerResult<IEnumerable<GameViewModel>>> GetUserGames()
        {
            return Result((await gameService.GetUserGames()).Convert(x=>x.Select(u=>new GameViewModel(u))));
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ControllerResult<GameInfoViewModel>> GetUserGame(int id)
        {
            return Result((await gameService.GetUserGame(id)).Convert(x => new GameInfoViewModel(x)));
        }
        [HttpGet]
        [Route("{id}/players")]
        public async Task<ControllerResult<List<GameUserViewModel>>> GetGamePalyers(int id)
        {
            return Result((await gameService.GetUserGame(id)).Convert(x => x.GameUsers.Select(u=>new GameUserViewModel(u,false)).ToList()));
        }
        [HttpPost]
        [Route("{id}/edit")]
        public async Task<ObjectControllerResult> EditGame(int id, GameEditModel model)
        {
            return Result(await gameService.EditGame(id, model));
        }
        [HttpPost]
        [Route("{id}/rounds/third/edit")]
        public async Task<ObjectControllerResult> EditThird(int id, [FromForm]ThirdRoundEditViewModel model)
        {
            return Result(await gameService.EditThirdRound(id, model));
        }
        [HttpGet]
        [Route("{id}/rounds/second")]
        public async Task<ControllerResult<SecondRoundViewModel>> GetGameSecondRound(int id)
        {
            return Result((await gameService.GetGameByPlayerRoles(id, PlayerRole.Creator)).Convert(x => new SecondRoundViewModel(x?.SecondRound)));
        }
        [HttpPost]
        [Route("{id}/rounds/second/edit")]
        public async Task<ObjectControllerResult> EditSecondRound(int id,SecondRoundEditViewModel model)
        {
            return Result(await gameService.EditSecondRound(id, model));
        }
        [HttpGet]
        [Route("{id}/rounds/first")]
        public async Task<ControllerResult<FirstRoundViewModel>> GetGameFirstRound(int id)
        {
            return Result((await gameService.GetGameByPlayerRoles(id, PlayerRole.Creator)).Convert(x => x?.FirstRound!=null? new FirstRoundViewModel(x?.FirstRound):null));
        }
        [HttpPost]
        [Route("{id}/rounds/first/edit")]
        public async Task<ObjectControllerResult> EditFirstRound(int id, FirstRoundEditViewModel model)
        {
            return Result(await gameService.EditFirstRound(id, model));
        }
        [HttpGet]
        [Route("{id}/start")]
        public async Task<ObjectControllerResult> CreateGameProcess(int id)
        {
            return Result(await gameService.CreateGameProcess(id));
        }



        [HttpGet]
        [Route("{id}/banners")]
        public async Task<ControllerResult<List<SponsorBannerViewModel>>> GetGameBanners(int id)
        {
            return Result((await gameService.GetSponsorBanners(id)).Convert(x => x.Select(u => new SponsorBannerViewModel(u)).ToList()));
        }
        [HttpGet]
        [Route("{gameId}/banners/add")]
        public async Task<ObjectControllerResult> AddBannerToGame(int gameId,int sponsorId)
        {
            return Result(await gameService.AddSponsorBannerToGame(gameId,sponsorId));
        }
    }
}