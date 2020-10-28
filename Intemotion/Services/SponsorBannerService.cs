using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Models;
using Intemotion.ViewModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface ISponsorBannerService
    {
        Task<ServiceResult> AddBannerToGame(int bannerId, int gameId);
        Task<ServiceResult> CreateBanner(SponsorBannerEditViewModel model);
        Task<ServiceResult> EditBanner(int id, SponsorBannerEditViewModel model);
        ServiceResult<List<SponsorBanner>> GetGameSponsorBanners(int gameId);
        Task<ServiceResult<SponsorBanner>> GetSponsorBanner(int id);
        Task<ServiceResult<List<SponsorBanner>>> GetSponsorBanners();
    }

    public class SponsorBannerService : BaseService, ISponsorBannerService
    {
        private readonly IAuthService authService;
        private readonly IFileService fileService;
        private readonly DataContext dataContext;

        public SponsorBannerService(IAuthService authService, IFileService fileService, DataContext dataContext)
        {
            this.authService = authService;
            this.fileService = fileService;
            this.dataContext = dataContext;
        }
        public async Task<ServiceResult<List<SponsorBanner>>> GetSponsorBanners()
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<List<SponsorBanner>>(user.ErrorMessage);

            return Success(dataContext.SponsorBanners.ToList());
        }
        public async Task<ServiceResult<SponsorBanner>> GetSponsorBanner(int id)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<SponsorBanner>(user.ErrorMessage);

            var banner = dataContext.SponsorBanners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return Error<SponsorBanner>("Не удалось найти баннер");
            }
            else
            {
                return Success(banner);
            }
        }
        public async Task<ServiceResult> CreateBanner(SponsorBannerEditViewModel model)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);
            ServiceResult<FileModel> file = null;
            if (model.File == null)
                return Error("Вы не выбрали файл");

            file = await fileService.SaveFile(model.File);
            if (!file.IsSuccess)
                return Error(file.ErrorMessage);

            var banner = new SponsorBanner
            {
                FileId = file.Data.Id,
                Name = model.Name,
                Url = model.Url
            };
            dataContext.Add(banner);
            await dataContext.SaveChangesAsync();
            return Success((object)banner.Id);
        }
        public async Task<ServiceResult> EditBanner(int id, SponsorBannerEditViewModel model)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);
            var bannerResult = await GetSponsorBanner(id);
            if (!bannerResult.IsSuccess)
                return Error(bannerResult.ErrorMessage);

            ServiceResult<FileModel> file = null;
            if (model.File != null)
            {
                file = await fileService.SaveFile(model.File);
                if (!file.IsSuccess)
                    return Error(file.ErrorMessage);
            }
            var banner = bannerResult.Data;

            banner.FileId = file == null ? banner.FileId : file.Data.Id;
            banner.Name = model.Name;
            banner.Url = model.Url;

            await dataContext.SaveChangesAsync();
            return Success((object)banner.Id);
        }
        public async Task<ServiceResult> AddBannerToGame(int bannerId,int gameId)
        {
            dataContext.SponsorBannerGames.Add(new SponsorBannerGame
            {
                SponsorBannerId = bannerId,
                GameId = gameId
            });
            await dataContext.SaveChangesAsync();
            return Success();
        }

        public ServiceResult<List<SponsorBanner>> GetGameSponsorBanners(int gameId)
        {
            return Success(dataContext.SponsorBannerGames.Where(x => x.GameId == gameId).Select(x => x.SponsorBanner).ToList());
        }
    }
}
