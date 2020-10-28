using Intemotion.Enums;

namespace Intemotion.Entities
{
    public class GameUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }

        public virtual User User { get; set; }
        public virtual Game Game { get; set; }

        public PlayerRole PlayerRole { get; set; }

    }
}
