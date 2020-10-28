using Intemotion.Entities;
using Intemotion.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class GameInfoViewModel
    {
        public GameInfoViewModel(Game model)
        {
            Id = model.Id;
            Name = model.Name;
            GameState = model.GameState;
            Description = model.Description;
            GameUsers = model.GameUsers.Select(x => new GameUserViewModel(x)).ToList();
            PlaneStartDate = model.PlaneStartDate;
            //MaxPlayersCount = model.MaxPlayersCount;
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public GameState GameState { get; set; }
        public List<GameUserViewModel> GameUsers { get; set; } = new List<GameUserViewModel>();
        public DateTime PlaneStartDate { get; set; }
        public int MaxPlayersCount { get; set; }
    }
}
