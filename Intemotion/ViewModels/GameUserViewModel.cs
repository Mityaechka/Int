using Intemotion.Entities;
using Intemotion.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class GameUserViewModel
    {
        public GameUserViewModel(GameUser model, bool includeGame = true)
        {
            User = new UserViewModel(model.User);
            if(includeGame)
            Game = new GameViewModel(model.Game);
            PlayerRole = model.PlayerRole;
        }
        public UserViewModel User { get; set; }
        public GameViewModel Game { get; set; }
        public PlayerRole PlayerRole{ get; set; }
    }
}
