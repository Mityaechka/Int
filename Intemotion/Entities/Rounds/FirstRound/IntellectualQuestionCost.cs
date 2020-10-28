namespace Intemotion.Entities.Rounds.FirstRound
{
    public class IntellectualQuestionCost
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
        public int? FirstRoundId { get; set; }
        public virtual FirstRound FirstRound { get; set; }
    }
}
