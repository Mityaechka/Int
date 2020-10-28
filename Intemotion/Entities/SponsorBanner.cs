using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities
{
    public class SponsorBanner
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public virtual FileModel File { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public virtual List<SponsorBannerGame> Games { get; set; }
    }
}
