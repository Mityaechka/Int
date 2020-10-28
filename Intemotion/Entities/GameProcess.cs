using Intemotion.Entities.Rounds.FirstRound;
using Intemotion.Entities.Rounds.SecondRound;
using Intemotion.Entities.Rounds.ThirdRound;
using Intemotion.Hubs.Models;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities
{
    public class GameProcess
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual List<ConnectedUser> ConnectedUsers { get; set; } = new List<ConnectedUser>();
        public DateTime CreateDate { get; set; }
        public DateTime StartTime { get; set; }

        public GameProcessState State { get; set; } = GameProcessState.WaitingStart;
        public int? FirstRoundResultId { get; set; }
        public virtual FirstRoundResult FirstRoundResult { get; set; }
        public int? SecondRoundResultId { get; set; }
        public virtual SecondRoundResult SecondRoundResult { get; set; }
        public int? ThirdRoundResultId { get; set; }
        public virtual ThirdRoundResult ThirdRoundResult { get; set; }
        [NotMapped]
        public List<string> ConnectionIds => ConnectedUsers.Select(x => x.ConnectionId).ToList();

        
    }

    public enum GameProcessState
    {
        WaitingStart,
        WaitingFirstRound,
        FirstRound, 
        FirstRoundAd,
        WaitingSecondRound,
        SecondRound,
        WaitingThirdRound,
        SecondRoundBreak,
        ThirdRound,
        ThirdRoundBreak,
    }
    public class FirstRoundResult
    {
        public int Id { get; set; }
        public int QuestionIndex { get; set; } = -1;
        public virtual List<IntellectualQuestion> MixedQuestions { get; set; }

        public virtual List<FirstRoundResulAnswer> Answers { get; set; }

        public PlayState State { get; set; }

        [NotMapped]
        public IntellectualQuestion CurrentQuestion => MixedQuestions[QuestionIndex];

        public DateTime AnswerCloseTime { get; set; }

    }
    public class FirstRoundResulAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IntellectualQuestion Question { get; set; }
        public virtual IntellectualAnswer Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
    public class SecondRoundResult
    {
        public int Id { get; set; }
        public int QuestionIndex { get; set; } = -1;
        public virtual List<TruthQuestion> MixedQuestions { get; set; }

        public virtual List<SecondRoundResulAnswer> Answers { get; set; }

        public PlayState State { get; set; }

        [NotMapped]
        public TruthQuestion CurrentQuestion => MixedQuestions[QuestionIndex];

    }
    public class SecondRoundResulAnswer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public virtual User User { get; set; }
        public virtual TruthQuestion Question { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
    public enum PlayState
    {
        SelectQuestion,
        ReceivingAnswers
    }

    public class ThirdRoundResult
    {
        public int Id { get; set; }



        public int QuestionIndex { get; set; } = -1;

        public PlayState State { get; set; }
        public ThirdRoundState ThirdRoundState { get; set; }


        public virtual List<ChronologyAnswer> ChronologyAnswers { get; set; } = new List<ChronologyAnswer>();
        public virtual List<AssociationAnswer> AssociationAnswers { get; set; } = new List<AssociationAnswer>();
        public virtual List<MelodyGuessAnswer> MelodyGuessAnswers { get; set; } = new List<MelodyGuessAnswer>();


        public DateTime AnswerCloseTime { get; set; }


    }
    public enum ThirdRoundState
    {
        Chronology = 0,
        MelodyGuess,
        Association
    }
    public class ChronologyAnswer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int? ChronologyId { get; set; }
        public virtual Chronology Chronology { get; set; }

        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
    public class MelodyGuessAnswer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int MelodyGuessVariantId { get; set; }
        public virtual MelodyGuessVariant MelodyGuessVariant { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
    public class AssociationAnswer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int AssociationVariantId { get; set; }
        public virtual AssociationVariant AssociationVariant { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
}
