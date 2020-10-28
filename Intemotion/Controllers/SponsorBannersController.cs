using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intemotion.Controllers
{
    [Route("api/banners")]
    [ApiController]
    public class SponsorBannersController : BaseController
    {
        private readonly ISponsorBannerService sponsorBannerService;

        public SponsorBannersController(ISponsorBannerService sponsorBannerService)
        {
            this.sponsorBannerService = sponsorBannerService;
        }
        [Route("")]
        public async  Task<ControllerResult<IEnumerable<SponsorBannerViewModel>>> GetSponsorBanners()
        {
            return Result((await sponsorBannerService.GetSponsorBanners()).Convert(x=>x.Select(x=>new SponsorBannerViewModel(x))));
        }
        [Route("{id}")]
        public async Task<ControllerResult<SponsorBannerViewModel>> GetSponsorBanner(int id)
        {
            return Result((await sponsorBannerService.GetSponsorBanner(id)).Convert(x =>  new SponsorBannerViewModel(x)));
        }
        [Route("create")]
        [HttpPost]
        public async Task<ObjectControllerResult> CreateBanner([FromForm]SponsorBannerEditViewModel model)
        {
            return Result(await sponsorBannerService.CreateBanner(model));
        }
        [Route("{id}/edit")]
        [HttpPost]
        public async Task<ObjectControllerResult> EditBanner(int id,SponsorBannerEditViewModel model)
        {
            return Result(await sponsorBannerService.EditBanner(id,model));
        }
        [Route("addToGame")]
        public async Task<ObjectControllerResult> AddBannerToGame(int bannerId, int gameId)
        {
            return Result(await sponsorBannerService.AddBannerToGame(bannerId, gameId));
        }
        [Route("gameBanners")]
        public ControllerResult<IEnumerable<SponsorBannerViewModel>> GetGameSponsorBanners( int gameId)
        {
            return Result(sponsorBannerService.GetGameSponsorBanners(gameId).Convert(x=>x.Select(u=>new SponsorBannerViewModel(u))));
        }
    }
}