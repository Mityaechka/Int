using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities.Rounds.ThirdRound
{
    public class ThirdRound
    {
        public int Id { get; set; }
        public virtual List<Chronology> Chronologies { get; set; }
        public virtual List<MelodyGuess> MelodyGuesses { get; set; }
        public virtual List<Association> Associations { get; set; }

    }
    public class Chronology
    {
        public int Id { get; set; }
        public int? ThirdRoundId { get; set; }
        public virtual ThirdRound ThirdRound { get; set; }
        public virtual List<ChronologyItem> ChronologyItems { get; set; }
    }
    public class ChronologyItem
    {
        public int Id { get; set; }
        public int? ChronologyId { get; set; }
        public virtual Chronology Chronology { get; set; }
        public int Index { get; set; }
        public int FileId { get; set; }
        public virtual FileModel File { get; set; }
        public string Value { get; set; }
    }
    public class MelodyGuess
    {
        public int Id { get; set; }
        public int? ThirdRoundId { get; set; }
        public virtual ThirdRound ThirdRound { get; set; }

        public int FileId { get; set; }
        public virtual FileModel File { get; set; }
        public virtual List<MelodyGuessVariant> MelodyGuessVariants { get; set; }

    }
    public class MelodyGuessVariant
    {
        public int Id { get; set; }
        public int? MelodyGuessId { get; set; }
        public virtual MelodyGuess MelodyGuess { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
    }
    public class Association
    {
        public int Id { get; set; }
        public int? ThirdRoundId { get; set; }
        public virtual ThirdRound ThirdRound { get; set; }

        public string Word { get; set; }
        public virtual List<AssociationVariant> AssociationVariants { get; set; }
    }
    public class AssociationVariant
    {
        public int Id { get; set; }
        public int? AssociationId { get; set; }
        public virtual Association Association { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
    }
}
