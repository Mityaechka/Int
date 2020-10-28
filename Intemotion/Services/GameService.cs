using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Entities.Rounds.FirstRound;
using Intemotion.Entities.Rounds.SecondRound;
using Intemotion.Entities.Rounds.ThirdRound;
using Intemotion.Enums;
using Intemotion.Hubs.Models;
using Intemotion.Models;
using Intemotion.Services.Notifications;
using Intemotion.ViewModels;
using Newtonsoft.Json.Schema;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface IGameService
    {
        Task<ServiceResult> AddSponsorBannerToGame(int gameId, int bannerId);
        Task<ServiceResult> AddVoucher(int gameID);
        Task<ServiceResult> BindModeratorToGame(int gameId, string moderatorId);
        Task<ServiceResult> ChangeGameState(int gameId, GameState gameState);
        Task<ServiceResult> ChangeGameStateToRecording(int gameId);
        Task<ServiceResult> CreateGame(GameEditModel model);
        Task<ServiceResult> CreateGameProcess(int gameId);
        Task<ServiceResult> EditFirstRound(int gameId, FirstRoundEditViewModel model);
        Task<ServiceResult> EditGame(int gameId, GameEditModel model);
        Task<ServiceResult> EditSecondRound(int gameId, SecondRoundEditViewModel model);
        Task<ServiceResult> EditThirdRound(int gameId, ThirdRoundEditViewModel model);
        Task<ServiceResult<List<Game>>> GetFreeGames();
        ServiceResult<Game> GetGame(int id);
        Task<ServiceResult<Game>> GetGameByPlayerRoles(int id, params PlayerRole[] playerRoles);
        ServiceResult<Game> GetGameByState(int id, GameState gameState);
        ServiceResult<Game> GetGameByState(DateTime date, GameState gameState);
        ServiceResult<List<Game>> GetGames();
        Task<ServiceResult<List<Game>>> GetGamesByPlayerRoles(params PlayerRole[] playerRoles);
        ServiceResult<List<Game>> GetGamesByState(GameState gameState);
        Task<ServiceResult<List<SponsorBanner>>> GetSponsorBanners(int gameId);
        Task<ServiceResult<Game>> GetUserGame(int gameId);
        Task<ServiceResult<List<Game>>> GetUserGames();
        Task<ServiceResult<List<Voucher>>> GetVouchers(int gameId);
        Task<ServiceResult> JoinToGameWithoutVoucher(int gameId);
        Task<ServiceResult> JoinToGameWithVoucher(DateTime date, string voucherCode);
    }

    public class GameService : BaseService, IGameService
    {
        private readonly DataContext context;
        private readonly IAuthService authService;
        private readonly IFileService fileService;

        public GameService(DataContext context, IAuthService authService, IFileService fileService)
        {
            this.context = context;
            this.authService = authService;
            this.fileService = fileService;
        }
        public async Task<ServiceResult> CreateGame(GameEditModel model)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                GameUsers = new List<GameUser> { new GameUser { UserId = user.Data.Id, PlayerRole = PlayerRole.Creator } },
                //MaxPlayersCount = model.MaxPlayersCount,
                PlaneStartDate = model.PlaneStartDate
            };

            context.Games.Add(game);

            await context.SaveChangesAsync();

            return Success((object)game.Id);
        }
        public async Task<ServiceResult> EditGame(int gameId, GameEditModel model)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);
            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            game.Data.Name = model.Name;
            game.Data.Description = model.Description;
            //game.Data.MaxPlayersCount =game.Data.GameUsers.Count< model.MaxPlayersCount? model.MaxPlayersCount:game.Data.MaxPlayersCount;
            game.Data.PlaneStartDate = model.PlaneStartDate;

            await context.SaveChangesAsync();

            return Success((object)game.Data.Id);
        }
        public async Task<ServiceResult> JoinToGameWithoutVoucher(int gameId)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var game = GetGameByState(gameId, GameState.RecordingPlaysers);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            if (game.Data.GameUsers.Any(x => x.UserId == user.Data.Id))
                return Error("Вы уже присоденились к игре");

            game.Data.GameUsers.Add(new GameUser { UserId = user.Data.Id });

            await context.SaveChangesAsync();

            return Success();
        }
        public async Task<ServiceResult> JoinToGameWithVoucher(DateTime date, string voucherCode)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var game = GetGameByState(date, GameState.RecordingPlaysers);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);
            if (game.Data.GameState != GameState.RecordingPlaysers)
                return Error("Еще нельзя присоедиться к этой игре");

            if (game.Data.GameUsers.Any(x => x.UserId == user.Data.Id))
                return Error("Вы уже присоденились к игре");
            var voucher = game.Data.Vouchers.FirstOrDefault(x => x.IsActive && x.UserId == null && x.Code == voucherCode);
            if (voucher == null)
                return Error("Не удалось найти ваучер с таким кодом");

            voucher.IsActive = false;
            voucher.UserId = user.Data.Id;

            game.Data.GameUsers.Add(new GameUser { UserId = user.Data.Id });

            await context.SaveChangesAsync();

            return Success();
        }
        public ServiceResult<List<Game>> GetGames()
        {
            var games = context.Games.ToList();
            return Success(games);
        }
        public async Task<ServiceResult<List<Game>>> GetFreeGames()
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<List<Game>>(user.ErrorMessage);

            var games = context.Games
                .Where(x => !x.GameUsers.Any(x => x.UserId == user.Data.Id) && x.GameState == GameState.RecordingPlaysers)
                .ToList();
            return Success(games);
        }
        public ServiceResult<Game> GetGame(int id)
        {
            var game = context.Games.FirstOrDefault(x => x.Id == id);

            if (game == null)
                return Error<Game>("Не удалось найти игру");
            else
                return Success(game);
        }
        public ServiceResult<List<Game>> GetGamesByState(GameState gameState)
        {
            var games = context.Games.Where(x => x.GameState == gameState).ToList();
            return Success(games);
        }
        public ServiceResult<Game> GetGameByState(DateTime date, GameState gameState)
        {
            var game = context.Games.FirstOrDefault(x => x.GameState == gameState && x.PlaneStartDate.Date.Date == date.Date);

            if (game == null)
                return Error<Game>("Не удалось найти игру");
            else
                return Success(game);
        }
        public ServiceResult<Game> GetGameByState(int id, GameState gameState)
        {
            var game = context.Games.FirstOrDefault(x => x.GameState == gameState && x.Id == id);

            if (game == null)
                return Error<Game>("Не удалось найти игру");
            else
                return Success(game);
        }
        public async Task<ServiceResult<List<Game>>> GetUserGames()
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<List<Game>>(user.ErrorMessage);

            var games = context.Games.Where(x => x.GameUsers.Any(x => x.UserId == user.Data.Id)).ToList();

            if (games == null)
                return Error<List<Game>>("Не удалось найти игру");
            else
                return Success(games);
        }
        public async Task<ServiceResult<Game>> GetUserGame(int gameId)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<Game>(user.ErrorMessage);

            var game = context.Games.FirstOrDefault(x => x.GameUsers.Any(x => x.UserId == user.Data.Id) && x.Id == gameId);

            if (game == null)
                return Error<Game>("Не удалось найти игру");
            else
                return Success(game);
        }

        public async Task<ServiceResult<List<Game>>> GetGamesByPlayerRoles(params PlayerRole[] playerRoles)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<List<Game>>(user.ErrorMessage);

            var games = context.Games.Where(x => x.GameUsers.Any(x => x.UserId == user.Data.Id))
                .ToList()
                .Where(x => x.GameUsers.Any(x => playerRoles.Any(u => u == x.PlayerRole))).ToList();

            if (games == null)
                return Error<List<Game>>("Не удалось найти игру");
            else
                return Success(games);
        }
        public async Task<ServiceResult<Game>> GetGameByPlayerRoles(int id, params PlayerRole[] playerRoles)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<Game>(user.ErrorMessage);

            var game = context.Games.Where(x => x.Id == id && x.GameUsers.Any(x => x.UserId == user.Data.Id))
                .ToList()
                .FirstOrDefault(x => x.GameUsers.Any(x => x.UserId == user.Data.Id && playerRoles.Any(u => u == x.PlayerRole)));

            if (game == null)
                return Error<Game>("Не удалось найти игру");
            else
                return Success(game);
        }

        public async Task<ServiceResult> AddVoucher(int gameID)
        {
            var game = await GetGameByPlayerRoles(gameID, PlayerRole.Creator);
            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            var voucher = new Voucher
            {
                GameId = gameID,
                IsActive = true
            };
            context.Vouchers.Add(voucher);
            await context.SaveChangesAsync();
            return Success((object)voucher.Id);
        }

        public async Task<ServiceResult> BindModeratorToGame(int gameId, string moderatorId)
        {

            var user = await authService.GetCurrentUser();

            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var moderator = await authService.GetUser(moderatorId);
            if (!moderator.IsSuccess)
                return Error(moderator.ErrorMessage);

            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            if (game.Data.GameUsers.Any(x => x.UserId == moderatorId))
                return Error("Данный пользователь уже присоединился к игре");

            game.Data.GameUsers.Add(new GameUser { UserId = moderatorId, PlayerRole = PlayerRole.Moderator });
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> ChangeGameStateToRecording(int gameId)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator, PlayerRole.Moderator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            if (game.Data.GameState != GameState.Create)
                return Error("Нельзя изменить текущее состояние игры");
            if (!game.Data.GameUsers.Any(x => x.PlayerRole == PlayerRole.Moderator))
                return Error("сначало назначьте модератора");

            game.Data.GameState = GameState.RecordingPlaysers;

            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> ChangeGameState(int gameId, GameState gameState)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator, PlayerRole.Moderator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            if (game.Data.GameState >= gameState)
                return Error("Нельзя изменить текущее состояние игры");

            game.Data.GameState = gameState;
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult<List<Voucher>>> GetVouchers(int gameId)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator, PlayerRole.Moderator);
            if (!game.IsSuccess)
                return Error<List<Voucher>>(game.ErrorMessage);

            return Success(game.Data.Vouchers);

        }
        public async Task<ServiceResult> EditSecondRound(int gameId, SecondRoundEditViewModel model)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);
            SecondRound secondRound = game.Data.SecondRound;
            if (secondRound == null)
            {
                secondRound = new SecondRound();
                game.Data.SecondRound = secondRound;
            }
            var index = 0;

            if (model.Questions.Count < secondRound.Questions.Count)
            {
                secondRound.Questions.RemoveRange(model.Questions.Count, secondRound.Questions.Count - model.Questions.Count);
            }
            var count = secondRound.Questions.Count;

            foreach (var questionModel in model.Questions)
            {
                if (index < count)
                {
                    var question = secondRound.Questions[index];
                    question.Value = model.Questions[index].Value;
                    question.IsTruth = model.Questions[index].IsTruth;
                    question.Index = index;
                }
                else
                {
                    var question = new TruthQuestion
                    {
                        Value = model.Questions[index].Value,
                        IsTruth = model.Questions[index].IsTruth,
                        Index = index
                    };
                    secondRound.Questions.Add(question);

                }
                index++;
            }
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> EditFirstRound(int gameId, FirstRoundEditViewModel model)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);
            FirstRound firstRound = game.Data.FirstRound;
            if (firstRound == null)
            {
                firstRound = new FirstRound();
                game.Data.FirstRound = firstRound;
            }

            //if (firstRound.QuestionCosts != null)
            //    context.IntellectualQuestionCosts.RemoveRange(firstRound.QuestionCosts);
            if (firstRound.QuestionsCategories != null)
                context.QuestionsCategorys.RemoveRange(firstRound.QuestionsCategories);
            await context.SaveChangesAsync();
            //firstRound.QuestionCosts = model.QuestionCosts.Select(x =>
            //{
            //    var cost = new IntellectualQuestionCost { Index = index, Value = x };
            //    index++;
            //    return cost;
            //}).ToList();

            firstRound.QuestionsCategories = model.QuestionsCategories.Select(x =>
             {
                 var questionIndex = 0;
                 var category = new QuestionsCategory
                 {
                     Name = x.Name,
                     Questions = x.Questions.Select(u =>
                     {
                         var answerIndex = 0;
                         var question = new IntellectualQuestion
                         {
                             Index = questionIndex,
                             Value = u.Value,
                             Answers = u.Answers.Select(t =>
                             {
                                 var answer = new IntellectualAnswer { Value = t, Index = answerIndex };
                                 answerIndex++;
                                 return answer;
                             }).ToList()
                         };
                         questionIndex++;
                         return question;
                     }).ToList()
                 };

                 return category;
             }).ToList();

            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> CreateGameProcess(int gameId)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);
            if (!game.IsSuccess)
                return Error(game.ErrorMessage);
            if (game.Data.GameState != GameState.RecordingPlaysers)
                return Error("Игровой процесс уже создан");
            if (context.GameProcesses.Any(x => x.GameId == gameId))
                return Error("Игровой процесс уже создан");
            game.Data.GameState = GameState.Playing;
            var gameProcess = new GameProcess
            {
                CreateDate = DateTime.Now,
                StartTime = game.Data.PlaneStartDate,
                GameId = gameId
            };
            context.GameProcesses.Add(gameProcess);
            await context.SaveChangesAsync();
            return Success((object)gameProcess.Id);
        }

        public async Task<ServiceResult<List<SponsorBanner>>> GetSponsorBanners(int gameId)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator, PlayerRole.Moderator);
            if (!game.IsSuccess)
                return Error<List<SponsorBanner>>(game.ErrorMessage);

            return Success(game.Data.SponsorBanners.Select(x => x.SponsorBanner).ToList());

        }
        public async Task<ServiceResult> AddSponsorBannerToGame(int gameId, int bannerId)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);
            if (!game.IsSuccess)
                return Error(game.ErrorMessage);
            var banner = context.SponsorBanners.FirstOrDefault(x => x.Id == bannerId);
            if (banner == null)
                return Error("Не удалось найти баннер");

            if (game.Data.SponsorBanners.Any(x => x.SponsorBannerId == bannerId))
                return Error("Баннер уже добавлен к игре");
            game.Data.SponsorBanners.Add(new SponsorBannerGame { SponsorBannerId = bannerId });
            await context.SaveChangesAsync();
            return Success();
        }




        public async Task<ServiceResult> EditThirdRound(int gameId, ThirdRoundEditViewModel model)
        {
            var game = await GetGameByPlayerRoles(gameId, PlayerRole.Creator);

            if (!game.IsSuccess)
                return Error(game.ErrorMessage);

            ThirdRound thirdRound = game.Data.ThirdRound;
            if (thirdRound == null)
            {
                thirdRound = new ThirdRound();
                game.Data.ThirdRound = thirdRound;
            }

            //if (firstRound.QuestionCosts != null)
            //    context.IntellectualQuestionCosts.RemoveRange(firstRound.QuestionCosts);
            if (thirdRound.Id != 0)
            {
                context.ThirdRounds.Remove(thirdRound);
                await context.SaveChangesAsync();
            }
            //firstRound.QuestionCosts = model.QuestionCosts.Select(x =>
            //{
            //    var cost = new IntellectualQuestionCost { Index = index, Value = x };
            //    index++;
            //    return cost;
            //}).ToList();

            thirdRound.Associations = model.Associations.Select(x =>
            {
                var index = 0;
                return new Association
                {
                    Word = x.Word,
                    AssociationVariants = x.AssociationVariants.Select(u =>
                    {
                        var variant = new AssociationVariant
                        {
                            Index = index,
                            Value = u
                        };
                        index++;
                        return variant;
                    }).ToList()
                };
            }).ToList();

            thirdRound.Chronologies = new List<Chronology>();
            foreach (var chronologyModel in model.Chronologies)
            {
                var chronology = new Chronology { ChronologyItems = new List<ChronologyItem>() };
                var index = 0;
                foreach (var chronologyItem in chronologyModel.ChronologyItems)
                {
                    var file = await fileService.SaveFile(chronologyItem.File);
                    if (file.IsSuccess)
                    {
                        chronology.ChronologyItems.Add(new ChronologyItem
                        {
                            FileId = file.Data.Id,
                            Value = chronologyItem.Value,
                            Index = index
                        });
                        index++;
                    }
                }
                thirdRound.Chronologies.Add(chronology);
            }
            thirdRound.MelodyGuesses= new List<MelodyGuess>();
            foreach (var melodyGuessEdit in model.MelodyGuesses)
            {
                var index = 0;
                var file = await fileService.SaveFile(melodyGuessEdit.File);
                if (file.IsSuccess)
                {
                    var guess = new MelodyGuess { MelodyGuessVariants = new List<MelodyGuessVariant>(), FileId = file.Data.Id };
                    foreach (var variant in melodyGuessEdit.MelodyGuessVariants)
                    {
                        guess.MelodyGuessVariants.Add(new MelodyGuessVariant
                        {
                            Index = index,
                            Value = variant
                        });
                        index++;
                    }
                    thirdRound.MelodyGuesses.Add(guess);

                }
            }
            await context.SaveChangesAsync();
            return Success();
        }
    }
}
