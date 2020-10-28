using Intemotion.Configs;
using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Enums;
using Intemotion.Hubs.Models;
using Intemotion.Models;
using Intemotion.SystemNotifications;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Schema;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface IGameProcessService
    {
        Task<ServiceResult> FirstRoundReceiveAnswer(int answerId);
        ServiceResult<ChatMessage> GetChatMessage(int id);
        Task<ServiceResult<ConnectedUser>> GetConnectedUser();
        ServiceResult<ConnectedUser> GetConnectedUser(string connectionId);
        ServiceResult<GameProcess> GetGameProcess(string connectionId);
        Task<ServiceResult<GameProcess>> GetGameProcess();
        ServiceResult<GameProcess> GetGameProcess(int id);
        Task<ServiceResult<int>> GetSecondRoundQuestionsCount();
        Task<ServiceResult> GetStatistic();
        Task<ServiceResult> JoinToGameProcess(string nickname = null);
        Task<ServiceResult> RemoveUserFromGame(string connectedId);
        Task<ServiceResult> ResetGame();
        Task Save();
        Task<ServiceResult> SecondRoundReceiveAnswer(bool isTruth);
        Task<ServiceResult<int>> SendSystemChatMessage(string value);
        Task<ServiceResult<int>> SendUserChatMessage(string value);
        Task<ServiceResult> StartFirstRound();
        Task<ServiceResult> StartGame();
        Task<ServiceResult> StartSecondRound();
        Task<ServiceResult> StartThirdRound();
        Task<ServiceResult> StartWaitingFirstRound();
        Task<ServiceResult> StartWaitingThirdRound();
        Task<ServiceResult> ThirdRoundAssociationReceiveAnswer(int answer);
        Task<ServiceResult> ThirdRoundChronologyReceiveAnswer(List<int> answers);
        Task<ServiceResult> ThirdRoundMelodyReceiveAnswer(int answer);
        Task<ServiceResult<bool>> TryAddUserToGame(int gameProcessId, string connectionId);
    }

    public class GameProcessService : BaseService, IGameProcessService
    {
        private readonly IGameService gameService;
        private readonly IAuthService authService;
        private readonly DataContext context;
        private readonly SystemNotification systemNotification;
        private readonly IOptions<RoundConfig> roundOptions;

        public GameProcessService(IGameService gameService, IAuthService authService, DataContext context, SystemNotification systemNotification, IOptions<RoundConfig> roundOptions)
        {
            this.gameService = gameService;
            this.authService = authService;
            this.context = context;
            this.systemNotification = systemNotification;
            this.roundOptions = roundOptions;
        }
        public async Task<ServiceResult<bool>> TryAddUserToGame(int gameProcessId, string connectionId)
        {
            var gameProcess = context.GameProcesses.FirstOrDefault(x => x.Id == gameProcessId);
            if (gameProcess == null)
                return Error<bool>("!!!");
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<bool>(user.ErrorMessage);

            var game = await gameService.GetGameByPlayerRoles(gameProcess.GameId, PlayerRole.Player, PlayerRole.Creator, PlayerRole.Moderator);
            if (!game.IsSuccess)
                return Error<bool>(game.ErrorMessage);

            if (context.ConnectedUsers.Any(x => x.ConnectionId == connectionId && x.GameProcessId == gameProcessId && x.UserId == user.Data.Id))
                return Error<bool>("Вы уже присоеденились к другой игру");
            //try
            //{
            //    context.ConnectedUsers.RemoveRange(context.ConnectedUsers.Where(x => x.UserId == user.Data.Id).ToArray());
            //    await context.SaveChangesAsync();
            //}catch(Exception e) { }
            context.ConnectedUsers.Add(new ConnectedUser
            {
                UserId = user.Data.Id,
                ConnectionId = connectionId,
                GameProcessId = gameProcessId
            });
            await context.SaveChangesAsync();
            return Success(true);
        }

        public async Task<ServiceResult> RemoveUserFromGame(string connectedId)
        {
            context.ConnectedUsers.RemoveRange(context.ConnectedUsers.Where(x => x.ConnectionId == connectedId));
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> JoinToGameProcess(string nickname = null)
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            user.Data.Nickname = nickname ?? user.Data.User.UserName;
            user.Data.IsJoin = true;

            await context.SaveChangesAsync();

            await SendSystemChatMessage($"Пользователь {user.Data.User.UserName} присоеденился к игре");
            systemNotification.CallEvent("SendHubEvent", new { eventName = "UserJoinGame", result = new ServiceResult { IsSuccess = true, Data = user.Data.ConnectionId } });
            return Success();
        }
        public async Task<ServiceResult> ResetGame()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            user.Data.GameProcess.State = GameProcessState.WaitingStart;
            ;
            //var round = user.Data.GameProcess?.FirstRoundResult;
            //if (round != null)
            //    round.QuestionIndex = 14;
            await context.SaveChangesAsync();

            return Success();
        }
        public async Task<ServiceResult> StartGame()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            if (process.Data.State != GameProcessState.WaitingStart)
                return Error("Данная игра уже началась");

            process.Data.State = GameProcessState.WaitingFirstRound;
            await context.SaveChangesAsync();

            return Success();
        }
        public async Task<ServiceResult<int>> SendUserChatMessage(string value)
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error<int>(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error<int>(process.ErrorMessage);

            var message = new ChatMessage
            {
                Value = value,
                ConnectedUserId = user.Data.Id,
                GameProcessId = process.Data.Id
            };

            context.ChatMessages.Add(message);

            await context.SaveChangesAsync();
            systemNotification.CallEvent("SendMessage", message.Id);
            return Success(message.Id);
        }
        public async Task<ServiceResult<int>> SendSystemChatMessage(string value)
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error<int>(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error<int>(process.ErrorMessage);

            var message = new ChatMessage
            {
                Value = value,
                ConnectedUserId = null,
                GameProcessId = process.Data.Id
            };

            context.ChatMessages.Add(message);

            await context.SaveChangesAsync();
            systemNotification.CallEvent("SendMessage", message.Id);
            return Success(message.Id);
        }
        public async Task<ServiceResult<ConnectedUser>> GetConnectedUser()
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error<ConnectedUser>(user.ErrorMessage);

            var connected = context.ConnectedUsers.Where(x => x.UserId == user.Data.Id).OrderByDescending(x => x.Id).FirstOrDefault();
            if (connected == null)
                return Error<ConnectedUser>("Не уалось найти игрока");
            else
                return Success(connected);
        }
        public ServiceResult<ConnectedUser> GetConnectedUser(string connectionId)
        {
            var connected = context.ConnectedUsers.Where(x => x.ConnectionId == connectionId).OrderByDescending(x => x.Id).FirstOrDefault();
            if (connected == null)
                return Error<ConnectedUser>("Не уалось найти игрока");
            else
                return Success(connected);
        }
        public async Task<ServiceResult> StartWaitingFirstRound()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            process.Data.State = GameProcessState.WaitingFirstRound;
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> StartFirstRound()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            process.Data.State = GameProcessState.FirstRound;
            var firstRound = new FirstRoundResult
            {
                MixedQuestions = process.Data.Game.FirstRound.QuestionsCategories.SelectMany(x => x.Questions).ToList()
            };
            process.Data.FirstRoundResult = firstRound;
            await context.SaveChangesAsync();
            await SendSystemChatMessage($"Начался первый раунд");
            return Success();
        }
        public async Task<ServiceResult> StartSecondRound()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            process.Data.State = GameProcessState.SecondRound;
            var secondRound = new SecondRoundResult
            {
                MixedQuestions = process.Data.Game.SecondRound.Questions
            };
            process.Data.SecondRoundResult = secondRound;
            await context.SaveChangesAsync();
            await SendSystemChatMessage($"Начался второй раунд");

            return Success();
        }
        public async Task<ServiceResult<int>> GetSecondRoundQuestionsCount()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error<int>(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error<int>(process.ErrorMessage);

            return Success<int>(process.Data.SecondRoundResult.MixedQuestions.Count);
        }
        public async Task<ServiceResult<GameProcess>> GetGameProcess()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error<GameProcess>(user.ErrorMessage);

            var process = context.GameProcesses.FirstOrDefault(x => x.ConnectedUsers.Any(u => u.ConnectionId == user.Data.ConnectionId));
            if (process == null)
                return Error<GameProcess>("Не уалось найти игру");
            else
                return Success(process);
        }
        public ServiceResult<ChatMessage> GetChatMessage(int id)
        {
            var message = context.ChatMessages.FirstOrDefault(x => x.Id == id);
            if (message == null)
                return Error<ChatMessage>("Не удалось найти сообщение");
            else
                return Success(message);
        }
        public ServiceResult<GameProcess> GetGameProcess(int id)
        {

            var process = context.GameProcesses.FirstOrDefault(x => x.Id == id);
            if (process == null)
                return Error<GameProcess>("Не уалось найти игру");
            else
                return Success(process);
        }
        public async Task<ServiceResult> FirstRoundReceiveAnswer(int answerId)
        {
            var time = DateTime.Now;

            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);
            if (process.Data.State != GameProcessState.FirstRound)
                return Error("Первый раунд закончился или еще не начался");

            if (process.Data.FirstRoundResult.State != PlayState.ReceivingAnswers)
                return Error("Все ответы уже приняты");

            var question = process.Data.FirstRoundResult.CurrentQuestion;
            if (!question.Answers.Any(x => x.Id == answerId))
                return Error("Ответ не найден");
            var answer = question.Answers.FirstOrDefault(x => x.Id == answerId);

            var score = 0;
            if (answer.Index == 0)
            {
                var delta = roundOptions.Value.FirstRoundReceivingAnswersInterval / 1000 - (process.Data.FirstRoundResult.AnswerCloseTime - time).TotalSeconds;
                score += roundOptions.Value.FirstRoundQuestionCost[question.Index];
                if (roundOptions.Value.FirstRoundBigScoreTime - delta >= 0)
                    score += roundOptions.Value.FirstRoundBigScoreValue;
                else if (roundOptions.Value.FirstRoundSmallScoreTime - delta >= 0)
                    score += roundOptions.Value.FirstRoundSmallScoreValue;
            }
            var answerResult = new FirstRoundResulAnswer
            {
                AnswerId = answerId,
                QuestionId = question.Id,
                IsCorrect = answer.Index == 0,
                Score = score,
                UserId = user.Data.Id
            };
            process.Data.FirstRoundResult.Answers.Add(answerResult);
            await SendSystemChatMessage($"Пользователь {user.Data.UserName} ответил на вопрос");
            return Success((object)score);
        }
        public async Task<ServiceResult> SecondRoundReceiveAnswer(bool isTruth)
        {
            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);
            if (process.Data.State != GameProcessState.SecondRound)
                return Error("Второй раунд закончился или еще не начался");

            if (process.Data.SecondRoundResult.State != PlayState.ReceivingAnswers)
                return Error("Все ответы уже приняты");

            var question = process.Data.SecondRoundResult.CurrentQuestion;

            var score = question.IsTruth == isTruth ? roundOptions.Value.SecondRoundCorrectAnswerScore : 0;
            var answerResult = new SecondRoundResulAnswer
            {
                QuestionId = question.Id,
                IsCorrect = question.IsTruth == isTruth,
                Score = score,
                UserId = user.Data.Id
            };
            process.Data.SecondRoundResult.Answers.Add(answerResult);

            await SendSystemChatMessage($"Пользователь {user.Data.UserName} ответил на вопрос");
            return Success((object)score);
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
        public ServiceResult<GameProcess> GetGameProcess(string connectionId)
        {
            var process = context.GameProcesses.FirstOrDefault(x => x.ConnectedUsers.Any(u => u.ConnectionId == connectionId));
            if (process == null)
                return Error<GameProcess>("Не уалось найти игру");
            else
                return Success(process);
        }
        public async Task<ServiceResult> StartWaitingThirdRound()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            process.Data.State = GameProcessState.WaitingThirdRound;
            await context.SaveChangesAsync();
            return Success();
        }
        public async Task<ServiceResult> StartThirdRound()
        {
            var user = await GetConnectedUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = GetGameProcess(user.Data.ConnectionId);
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            process.Data.State = GameProcessState.ThirdRound;

            process.Data.ThirdRoundResult = new ThirdRoundResult();/* { ThirdRoundState = ThirdRoundState.MelodyGuess,QuestionIndex = 0};*/
            await context.SaveChangesAsync();
            await SendSystemChatMessage($"Начался первый раунд");
            return Success();
        }
        public async Task<ServiceResult> ThirdRoundChronologyReceiveAnswer(List<int> answers)
        {
            var time = DateTime.Now;

            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);
            if (process.Data.State != GameProcessState.ThirdRound)
                return Error("Первый раунд закончился или еще не начался");

            if (process.Data.ThirdRoundResult.State != PlayState.ReceivingAnswers)
                return Error("Все ответы уже приняты");
            if (process.Data.ThirdRoundResult.ThirdRoundState != ThirdRoundState.Chronology)
                return Error("Ассоциации уже закончились");

            var chronology = process.Data.Game.ThirdRound.Chronologies[process.Data.ThirdRoundResult.QuestionIndex];

            if (!chronology.ChronologyItems.Any(x => answers.Any(u => u == x.Id)))
                return Error("Ответ не найден");
            var c = chronology.ChronologyItems.OrderByDescending(x => x.Index).Select(x => x.Id);
            var isCorrect = chronology.ChronologyItems.OrderByDescending(x => x.Index).Select(x => x.Id).SequenceEqual(answers);
            var score = isCorrect ? roundOptions.Value.ThirdRoundChronologyScore : 0;


            var answerResult = new ChronologyAnswer
            {
                IsCorrect = isCorrect,
                Score = score,
                UserId = user.Data.Id,
                ChronologyId=chronology.Id
            };
            process.Data.ThirdRoundResult.ChronologyAnswers.Add(answerResult);

            await SendSystemChatMessage($"Пользователь {user.Data.UserName} ответил на вопрос");

            return Success((object)score);
        }
        public async Task<ServiceResult> ThirdRoundMelodyReceiveAnswer(int answer)
        {
            var time = DateTime.Now;

            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);
            if (process.Data.State != GameProcessState.ThirdRound)
                return Error("Первый раунд закончился или еще не начался");

            if (process.Data.ThirdRoundResult.State != PlayState.ReceivingAnswers)
                return Error("Все ответы уже приняты");
            if (process.Data.ThirdRoundResult.ThirdRoundState != ThirdRoundState.MelodyGuess)
                return Error("Ассоциации уже закончились");

            var melody = process.Data.Game.ThirdRound.MelodyGuesses[process.Data.ThirdRoundResult.QuestionIndex];

            if (!melody.MelodyGuessVariants.Any(x => x.Id != answer))
                return Error("Ответ не найден");
            var isCorrect = melody.MelodyGuessVariants.FirstOrDefault(x => x.Index == 0).Id == answer;
            var score = isCorrect ? roundOptions.Value.ThirdRoundMelodyScore : 0;


            var answerResult = new MelodyGuessAnswer
            {
                IsCorrect = isCorrect,
                Score = score,
                UserId = user.Data.Id,
                MelodyGuessVariantId = answer
            };
            process.Data.ThirdRoundResult.MelodyGuessAnswers.Add(answerResult);

            await SendSystemChatMessage($"Пользователь {user.Data.UserName} ответил на вопрос");

            return Success((object)score);
        }
        public async Task<ServiceResult> ThirdRoundAssociationReceiveAnswer(int answer)
        {
            var time = DateTime.Now;

            var user = await authService.GetCurrentUser();
            if (!user.IsSuccess)
                return Error(user.ErrorMessage);

            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);
            if (process.Data.State != GameProcessState.ThirdRound)
                return Error("Первый раунд закончился или еще не начался");

            if (process.Data.ThirdRoundResult.State != PlayState.ReceivingAnswers)
                return Error("Все ответы уже приняты");
            if (process.Data.ThirdRoundResult.ThirdRoundState != ThirdRoundState.Association)
                return Error("Ассоциации уже закончились");

            var association = process.Data.Game.ThirdRound.Associations[process.Data.ThirdRoundResult.QuestionIndex];

            if (!association.AssociationVariants.Any(x => x.Id != answer))
                return Error("Ответ не найден");
            var isCorrect = association.AssociationVariants.FirstOrDefault(x => x.Index == 0).Id == answer;
            var score = isCorrect ? roundOptions.Value.ThirdRoundAssociationScore : 0;


            var answerResult = new AssociationAnswer
            {
                IsCorrect = isCorrect,
                Score = score,
                UserId = user.Data.Id,
                AssociationVariantId = answer
            };
            process.Data.ThirdRoundResult.AssociationAnswers.Add(answerResult);

            await SendSystemChatMessage($"Пользователь {user.Data.UserName} ответил на вопрос");

            return Success((object)score);
        }

        public async Task<ServiceResult> GetStatistic()
        {
            var process = await GetGameProcess();
            if (!process.IsSuccess)
                return Error(process.ErrorMessage);

            var connectedUsers = process.Data.ConnectedUsers;

            var firstRoundResult = process.Data.FirstRoundResult;
            var secondRoundResult = process.Data.SecondRoundResult;
            var thirdRoundResult = process.Data.ThirdRoundResult;

            var data = new
            {
                Users = connectedUsers.Select(x => new { x.User.Id, x.Nickname }),
                FirstRound = firstRoundResult?.Answers.GroupBy(x => x.UserId).Select(x => new { UserId = x.Key, Answers = x.Select(u => new { u.IsCorrect, u.Score, u.QuestionId }) }),
                SecondRound = secondRoundResult?.Answers.GroupBy(x => x.UserId).Select(x => new { UserId = x.Key, Answers = x.Select(u => new { u.IsCorrect, u.Score, u.QuestionId }) }),
                ThirdRound = thirdRoundResult.Select(x => new
                {
                    ChronologyAnswer = x.ChronologyAnswers?.GroupBy(u => u.UserId).Select(u => new { UserId = u.Key, Answers = u.Select(t => new { t.Score, t.IsCorrect, t.ChronologyId }) }),
                    AssociationAnswers = x.AssociationAnswers?.GroupBy(u => u.UserId).Select(u => new { UserId = u.Key, Answers = u.Select(t => new { t.Score, t.IsCorrect, t.AssociationVariant.AssociationId }) }),
                    MelodyGuessAnswers = x.MelodyGuessAnswers?.GroupBy(u => u.UserId).Select(u => new { UserId = u.Key, Answers = u.Select(t => new { t.Score, t.IsCorrect, t.MelodyGuessVariant.MelodyGuessId }) }),
                })//thirdRoundResult?.Answers.GroupBy(x => x.UserId).Select(x => new { UserId = x.Key, Answers = x.Select(u => u) }),
            };
            return Success((object)data);
        }
    }
    public static class Extensions
    {
        public static U Select<T, U>(this T item, Func<T, U> func)
        {
            if (item == null)
                return default;

            return func(item);
        }
    }
}
