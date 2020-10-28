using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities.Rounds.SecondRound
{
    public class SecondRound
    {
        public int Id { get; set; }
        public virtual List<TruthQuestion> Questions { get; set; } = new List<TruthQuestion>();
    }
}
