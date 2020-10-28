using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities
{
    public class SponsorBannerGame
    {
        public int SponsorBannerId { get; set; }
        public int GameId { get; set; }
        public virtual SponsorBanner SponsorBanner{ get; set; }
        public virtual Game Game { get; set; }
    }
}
