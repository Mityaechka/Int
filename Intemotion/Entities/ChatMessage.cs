using Intemotion.Hubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int? ConnectedUserId { get; set; }
        public virtual ConnectedUser ConnectedUser { get; set; }
        public int GameProcessId { get; set; }
        public virtual GameProcess GameProcess { get; set; }
        public string Value { get; set; }

    }
    public enum ChatMessageType
    {

    }
}
