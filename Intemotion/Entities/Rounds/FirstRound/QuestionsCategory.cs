using System.Collections.Generic;

namespace Intemotion.Entities.Rounds.FirstRound
{
    public class QuestionsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<IntellectualQuestion> Questions { get; set; }
        public int? FirstRoundId { get; set; }
        public virtual  FirstRound FirstRound { get; set; }
    }
}
