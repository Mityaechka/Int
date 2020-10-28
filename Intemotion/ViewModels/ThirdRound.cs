using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class ThirdRoundEditViewModel
    {
        public virtual List<ChronologyEditViewModel> Chronologies { get; set; }
        public virtual List<MelodyGuessEditViewModel> MelodyGuesses { get; set; }
        public virtual List<AssociationEditViewModel> Associations { get; set; }

    }
    public class ChronologyEditViewModel
    {
        public virtual List<ChronologyItemEditViewModel> ChronologyItems { get; set; }
    }
    public class ChronologyItemEditViewModel
    {
        public  IFormFile File { get; set; }
        public string Value { get; set; }
    }
    public class MelodyGuessEditViewModel
    {
        public IFormFile File { get; set; }
        public List<string> MelodyGuessVariants { get; set; }

    }

    public class AssociationEditViewModel
    {
        public string Word { get; set; }
        public virtual List<string> AssociationVariants { get; set; }
    }
}
