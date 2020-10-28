using System.Collections.Generic;

namespace Intemotion.Entities.Rounds.FirstRound
{
    public class FirstRound
    {
        public int Id { get; set; }
        public virtual List<QuestionsCategory> QuestionsCategories{ get; set; }
        //public virtual List<IntellectualQuestionCost> QuestionCosts { get; set; }
    }
}
