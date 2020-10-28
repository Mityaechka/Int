using Intemotion.Entities.Rounds.FirstRound;
using Intemotion.Entities.Rounds.SecondRound;
using Intemotion.Entities.Rounds.ThirdRound;
using Intemotion.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intemotion.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public GameState GameState{ get; set; }

        public virtual List<GameUser> GameUsers { get; set; } = new List<GameUser>();
        public virtual List<Voucher> Vouchers { get; set; } = new List<Voucher>();

        public DateTime PlaneStartDate{ get; set; }


        public int? SecondRoundId { get; set; }
        public virtual SecondRound SecondRound { get; set; }

        public int? FirstRoundId { get; set; }
        public virtual FirstRound FirstRound { get; set; }

        public int? ThirdRoundId { get; set; }
        public virtual ThirdRound ThirdRound { get; set; }

        public virtual List<SponsorBannerGame> SponsorBanners { get; set; }
    }
}
