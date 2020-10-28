namespace Intemotion.Entities.Rounds.FirstRound
{
    public class IntellectualAnswer
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
        public int? IntellectualQuestionId { get; set; }
        public virtual  IntellectualQuestion IntellectualQuestion { get; set; }
    }
}
