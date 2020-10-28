using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class GameEditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PlaneStartDate { get; set; }
        public int MaxPlayersCount { get; set; }
    }

}
