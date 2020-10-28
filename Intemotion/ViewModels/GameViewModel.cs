using Intemotion.Entities;
using Intemotion.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(Game model)
        {
            Id = model.Id;
            Name = model.Name;
            GameState = model.GameState;
            PlayersCount = model.GameUsers.Count;
            PlaneStartDate = model.PlaneStartDate;
            //MaxPlayersCount = model.MaxPlayersCount;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public GameState GameState{ get; set; }
        public int PlayersCount { get; set; }
        public DateTime PlaneStartDate { get; set; }
        public int MaxPlayersCount { get; set; }

    }
}
