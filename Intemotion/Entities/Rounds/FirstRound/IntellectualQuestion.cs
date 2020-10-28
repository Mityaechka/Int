using System.Collections.Generic;

namespace Intemotion.Entities.Rounds.FirstRound
{
    public class IntellectualQuestion
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual List<IntellectualAnswer> Answers { get; set; }
        public int Index { get; set; }
        public int? QuestionsCategoryId { get; set; }
        public virtual QuestionsCategory QuestionsCategory { get; set; }

        public virtual List<FirstRoundResulAnswer> FirstRoundResulAnswers{ get; set; }
    }
}
