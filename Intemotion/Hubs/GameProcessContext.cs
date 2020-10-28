using Castle.DynamicProxy.Generators;
using Intemotion.Configs;
using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Hubs.Extensions;
using Intemotion.Hubs.Models;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Intemotion.Hubs
{
    public class GameProcessContext
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private IGameProcessService gameProcessService;
        private IHubContext<GameProcessHub> hubContext;
        private DataContext dataContext;
        private readonly IOptions<RoundConfig> roundOptions;

        public GameProcessContext(IServiceScopeFactory serviceScopeFactory,
                                  IGameProcessService gameProcessService,
                                  IHubContext<GameProcessHub> hubContext,
                                  DataContext dataContext,
                                  IOptions<RoundConfig> roundOptions)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.gameProcessService = gameProcessService;
            this.hubContext = hubContext;
            this.dataContext = dataContext;
            this.roundOptions = roundOptions;
        }
        public async Task SendChatMessage(int messageId)
        {
            var gameProcess = await gameProcessService.GetGameProcess();
            if (gameProcess.IsSuccess)
            {
                var message = gameProcessService.GetChatMessage(messageId);
                if (message.IsSuccess)
                {
                    await hubContext.Clients.Clients(gameProcess.Data.ConnectionIds).SendEvent("SendChatMessage", new ServiceResult<ChatMessageViewModel>
                    {
                        IsSuccess = true,
                        Data = new ChatMessageViewModel(message.Data)
                    });
                }
            }
        }
        public async Task SendGameState(int gameProcessId)
        {
            var gameProcess = gameProcessService.GetGameProcess(gameProcessId);
            if (gameProcess.IsSuccess)
            {

                await hubContext.Clients.Clients(gameProcess.Data.ConnectionIds).SendEvent("GetGameState", new ServiceResult<GameProcessState>
                {
                    IsSuccess = true,
                    Data = gameProcess.Data.State
                });
            }
        }
        public async Task SendGameState()
        {
            var gameProcess = await gameProcessService.GetGameProcess();
            if (gameProcess.IsSuccess)
            {

                await hubContext.Clients.Clients(gameProcess.Data.ConnectionIds).SendEvent("GetGameState", new ServiceResult<GameProcessState>
                {
                    IsSuccess = true,
                    Data = gameProcess.Data.State
                });
            }
        }
        public async Task Error<T>(string connectioId, ServiceResult<T> result)
        {
            await hubContext.Clients.Client(connectioId).SendAsync("event", new HubEvent("Error", result.ToObject()));
        }
        public async Task PlayFirstRound()
        {
            var process = await gameProcessService.GetGameProcess();

            if (process.IsSuccess)
            {
                FirstRoundSelectQuestion(process.Data.Id);
            }
        }

        private async void FirstRoundSelectQuestion(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);
            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.FirstRoundReceivingAnswersInterval / 1000);
                process.Data.FirstRoundResult.QuestionIndex++;
                if (process.Data.FirstRoundResult.QuestionIndex == process.Data.FirstRoundResult.MixedQuestions.Count)
                {
                    FirstRoundShowAd(process.Data.Id);
                    return;
                }
                process.Data.FirstRoundResult.State = PlayState.SelectQuestion;

                await gameProcessService.Save();
                var lastQuestion = process.Data.FirstRoundResult.QuestionIndex == 0 ? null : process.Data.FirstRoundResult.MixedQuestions[process.Data.FirstRoundResult.QuestionIndex - 1]?.Answers?.FirstOrDefault(x => x.Index == 0);
                await SendEvent(process.Data, "FirstRoundSelectQuestion", new ServiceResult<object>
                {
                    IsSuccess = true,
                    Data = new
                    {
                        Time = time,
                        CorrectAnswer = lastQuestion != null ? new { lastQuestion.Id, lastQuestion.Value } : null
                    }
                });

                var timer = new Timer
                {
                    Interval = roundOptions.Value.FirstRoundReceivingAnswersInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => FirstRoundReceivingAnswers(process.Data.Id);
                timer.Start();
            }
        }
        private async void FirstRoundReceivingAnswers(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();

            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.FirstRoundReceivingAnswersInterval / 1000);
                var question = process.Data.FirstRoundResult.MixedQuestions[process.Data.FirstRoundResult.QuestionIndex];

                process.Data.FirstRoundResult.State = PlayState.ReceivingAnswers;
                process.Data.FirstRoundResult.AnswerCloseTime = time;

                await gameProcessService.Save();
                await SendEvent(process.Data, "FirstRoundReceivingAnswers", new ServiceResult<FirstRoundQuestionViewModel>
                {
                    IsSuccess = true,
                    Data = new FirstRoundQuestionViewModel(time, question, roundOptions.Value.FirstRoundQuestionCost)
                });

                var timer = new Timer
                {
                    Interval = roundOptions.Value.FirstRoundReceivingAnswersInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => FirstRoundSelectQuestion(gameProcessId);
                timer.Start();
            }
        }
        private async void FirstRoundShowAd(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                process.Data.State = GameProcessState.FirstRoundAd;
                await gameProcessService.Save();
                await SendGameState(process.Data.Id);

                var timer = new Timer
                {
                    Interval = roundOptions.Value.FirstRoundAdInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => SecondRoundWaitStart(gameProcessId);
                timer.Start();
            }
        }
        private async void SecondRoundWaitStart(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                process.Data.State = GameProcessState.WaitingSecondRound;
                await gameProcessService.Save();
                await SendGameState(process.Data.Id);
            }
        }

        public async Task PlaySecondRound()
        {
            var process = await gameProcessService.GetGameProcess();

            if (process.IsSuccess)
            {
                //var timer = new Timer
                //{
                //    Interval = 1000,
                //    AutoReset = false
                //};
                //timer.Elapsed += (sender, e) => SecondRoundSelectQuestion(process.Data.Id);
                //timer.Start();
                SecondRoundSelectQuestion(process.Data.Id);

            }
        }
        private async void SecondRoundSelectQuestion(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.SecondRoundSelectQuestionInterval / 1000);
                process.Data.SecondRoundResult.QuestionIndex++;
                if (process.Data.SecondRoundResult.QuestionIndex == process.Data.SecondRoundResult.MixedQuestions.Count)
                {
                    SecondRoundBreak(process.Data.Id);
                    return;
                }
                process.Data.SecondRoundResult.State = PlayState.SelectQuestion;

                await gameProcessService.Save();

                var lastQuestion = process.Data.SecondRoundResult.QuestionIndex == 0 ? null : process.Data.SecondRoundResult.MixedQuestions[process.Data.SecondRoundResult.QuestionIndex - 1];

                await SendEvent(process.Data, "SecondRoundSelectQuestion", new ServiceResult<object>
                {
                    IsSuccess = true,
                    Data = new { Time = time, CorrectAnswer = lastQuestion == null ? null : new { lastQuestion.Id, lastQuestion.IsTruth } }
                });

                var timer = new Timer
                {
                    Interval = roundOptions.Value.SecondRoundSelectQuestionInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => SecondRoundReceivingAnswers(process.Data.Id);
                timer.Start();
            }
        }
        private async void SecondRoundReceivingAnswers(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();

            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.SecondRoundReceivingAnswersInterval / 1000);
                var question = process.Data.SecondRoundResult.MixedQuestions[process.Data.SecondRoundResult.QuestionIndex];

                process.Data.SecondRoundResult.State = PlayState.ReceivingAnswers;

                await gameProcessService.Save();
                await SendEvent(process.Data, "SecondRoundReceivingAnswers", new ServiceResult<SecondRoundQuestionViewModel>
                {
                    IsSuccess = true,
                    Data = new SecondRoundQuestionViewModel(time, question)
                }); ;

                var timer = new Timer
                {
                    Interval = roundOptions.Value.SecondRoundReceivingAnswersInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => SecondRoundSelectQuestion(gameProcessId);
                timer.Start();
            }
        }
        private async void SecondRoundBreak(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                process.Data.State = GameProcessState.WaitingThirdRound;
                await gameProcessService.Save();
                await SendGameState(process.Data.Id);
                //await SendEvent(process.Data, "SecondRoundWait", new ServiceResult<object>
                //{
                //    IsSuccess = true,
                //    Data = null
                //});
            }
        }




        public async Task PlayThirdRound()
        {
            var process = await gameProcessService.GetGameProcess();

            if (process.IsSuccess)
            {
                ThirdRoundSelectQuestion(process.Data.Id);
            }
        }
        private async void ThirdRoundSelectQuestion(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);
            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.ThirdRoundSelectQuestionInterval / 1000);
                process.Data.ThirdRoundResult.QuestionIndex++;

                switch (process.Data.ThirdRoundResult.ThirdRoundState)
                {
                    case ThirdRoundState.Chronology:
                        var lastChronology = process.Data.ThirdRoundResult.QuestionIndex == 0 ? null : process.Data.Game.ThirdRound.Chronologies[process.Data.ThirdRoundResult.QuestionIndex - 1];
                        await SendEvent(process.Data, "ThirdRoundSelectQuestion", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                Chronology = lastChronology != null ? new
                                {
                                    Items = lastChronology.ChronologyItems.OrderBy(x => x.Index).Select(x => new
                                    {
                                        x.File.Path,
                                        x.Value,
                                        x.Index
                                    })
                                } : null
                            }
                        });
                        break;
                    case ThirdRoundState.MelodyGuess:
                        var lastMelody = process.Data.ThirdRoundResult.QuestionIndex == 0 ? null : process.Data.Game.ThirdRound.MelodyGuesses[process.Data.ThirdRoundResult.QuestionIndex - 1];
                        await SendEvent(process.Data, "ThirdRoundSelectQuestion", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                Melody = lastMelody != null ? new
                                {
                                    CorrectAnswer = new
                                    {
                                        lastMelody.MelodyGuessVariants.FirstOrDefault(x => x.Index == 0).Value,
                                        lastMelody.MelodyGuessVariants.FirstOrDefault(x => x.Index == 0).Id
                                    }
                                } : null
                            }
                        });
                        break;
                    case ThirdRoundState.Association:
                        var lastAssociation = process.Data.ThirdRoundResult.QuestionIndex == 0 ? null : process.Data.Game.ThirdRound.Associations[process.Data.ThirdRoundResult.QuestionIndex - 1];
                        await SendEvent(process.Data, "ThirdRoundSelectQuestion", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                Association = lastAssociation != null ? new
                                {
                                    CorrectAnswer = new
                                    {
                                        lastAssociation.AssociationVariants.FirstOrDefault(x => x.Index == 0).Value,
                                        lastAssociation.AssociationVariants.FirstOrDefault(x => x.Index == 0).Id
                                    }
                                } : null
                            }
                        });
                        break;
                }

                switch (process.Data.ThirdRoundResult.ThirdRoundState)
                {
                    case ThirdRoundState.Chronology:
                        if (process.Data.ThirdRoundResult.QuestionIndex == process.Data.Game.ThirdRound.Chronologies.Count)
                        {
                            process.Data.ThirdRoundResult.QuestionIndex = 0;
                            process.Data.ThirdRoundResult.ThirdRoundState = ThirdRoundState.MelodyGuess;
                        }
                        break;
                    case ThirdRoundState.MelodyGuess:
                        if (process.Data.ThirdRoundResult.QuestionIndex == process.Data.Game.ThirdRound.MelodyGuesses.Count)
                        {
                            process.Data.ThirdRoundResult.QuestionIndex = 0;
                            process.Data.ThirdRoundResult.ThirdRoundState = ThirdRoundState.Association;
                        }
                        break;
                    case ThirdRoundState.Association:
                        if (process.Data.ThirdRoundResult.QuestionIndex == process.Data.Game.ThirdRound.Associations.Count)
                        {
                            //FirstRoundShowAd(process.Data.Id);
                            ThirdRoundBreak(gameProcessId);
                            return;
                        }
                        break;
                }
                
                process.Data.ThirdRoundResult.State = PlayState.SelectQuestion;

                await gameProcessService.Save();



                var timer = new Timer
                {
                    Interval = roundOptions.Value.ThirdRoundReceivingAnswersInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => ThirdRoundReceivingAnswers(process.Data.Id);
                timer.Start();
            }
        }
        private async void ThirdRoundReceivingAnswers(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();

            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                var time = DateTime.Now.AddSeconds(roundOptions.Value.ThirdRoundReceivingAnswersInterval / 1000);

                process.Data.ThirdRoundResult.State = PlayState.ReceivingAnswers;
                process.Data.ThirdRoundResult.AnswerCloseTime = time;

                await gameProcessService.Save();

                switch (process.Data.ThirdRoundResult.ThirdRoundState)
                {
                    case ThirdRoundState.Chronology:
                        var question = process.Data.Game.ThirdRound.Chronologies[process.Data.ThirdRoundResult.QuestionIndex];
                        await SendEvent(process.Data, "ThirdRoundReceivingAnswers", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                ChronologyItems = question.ChronologyItems.Select(x => new
                                {
                                    x.File.Path,
                                    x.Value,
                                    x.Id
                                })
                            }
                        });
                        break;
                    case ThirdRoundState.MelodyGuess:
                        var melody = process.Data.Game.ThirdRound.MelodyGuesses[process.Data.ThirdRoundResult.QuestionIndex];
                        await SendEvent(process.Data, "ThirdRoundReceivingAnswers", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                FilePath = melody.File.Path,
                                MelodyGuessVariants = melody.MelodyGuessVariants.Select(x => new
                                {
                                    x.Value,
                                    x.Id
                                })
                            }
                        });
                        break;
                    case ThirdRoundState.Association:
                        var association = process.Data.Game.ThirdRound.Associations[process.Data.ThirdRoundResult.QuestionIndex];
                        await SendEvent(process.Data, "ThirdRoundReceivingAnswers", new ServiceResult<object>
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                State = process.Data.ThirdRoundResult.ThirdRoundState,
                                Time = time,
                                Word = association.Word,
                                AssociationVariants = association.AssociationVariants.Select(x => new
                                {
                                    x.Value,
                                    x.Id
                                })
                            }
                        });
                        break;
                }

                var timer = new Timer
                {
                    Interval = roundOptions.Value.ThirdRoundReceivingAnswersInterval,
                    AutoReset = false
                };
                timer.Elapsed += (sender, e) => ThirdRoundSelectQuestion(gameProcessId);
                timer.Start();
            }
        }

        private async void ThirdRoundBreak(int gameProcessId)
        {
            gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            dataContext = serviceScopeFactory.CreateScope().ServiceProvider.GetService<DataContext>();
            var process = gameProcessService.GetGameProcess(gameProcessId);

            if (process.IsSuccess)
            {
                process.Data.State = GameProcessState.ThirdRoundBreak;
                await gameProcessService.Save();
                await SendGameState(process.Data.Id);
                //await SendEvent(process.Data, "SecondRoundWait", new ServiceResult<object>
                //{
                //    IsSuccess = true,
                //    Data = null
                //});
            }
        }












        public async Task SendEvent<T>(string eventName, ServiceResult<T> result)
        {
            var gameProcessService = serviceScopeFactory.CreateScope().ServiceProvider.GetService<IGameProcessService>();
            var user = await gameProcessService.GetConnectedUser();
            if (user.IsSuccess)
            {
                await hubContext.Clients.Clients(user.Data.GameProcess.ConnectionIds).SendEvent(eventName, result);
            }
        }
        public async Task SendEvent<T>(GameProcess gameProcess, string eventName, ServiceResult<T> result)
        {
            await hubContext.Clients.Clients(gameProcess.ConnectionIds).SendEvent(eventName, result);
        }
    }

}
