using Intemotion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Hubs.Models
{
    public class ConnectedUser
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public virtual Entities.User User { get; set; }
        public int GameProcessId { get; set; }
        public virtual GameProcess GameProcess { get; set; }

        public string Nickname { get; set; }
        public bool IsJoin { get; set; }


        public virtual List<ChatMessage> ChatMessages { get; set; }
    }
}
