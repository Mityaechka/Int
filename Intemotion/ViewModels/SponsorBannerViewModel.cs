
using Intemotion.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class SponsorBannerViewModel
    {
        public SponsorBannerViewModel(SponsorBanner model) {
            Id = model.Id;
            if (model.File != null)
                File = new FileViewModel(model.File);

            Url = model.Url;
            Name = model.Name;
        }
        public int Id { get; set; }
        public virtual FileViewModel File { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
    public class SponsorBannerEditViewModel
    {
        public virtual IFormFile File { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
