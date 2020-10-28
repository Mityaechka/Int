using Intemotion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class ChatMessageViewModel
    {
        public ChatMessageViewModel(ChatMessage model)
        {
            Id = model.Id;
            IsSystem = model.ConnectedUser == null;
            if(model.ConnectedUser != null)
            {
                Nickname = model.ConnectedUser.Nickname;
            }
            Value = model.Value;
        }
        public int Id { get; set; }
        public bool IsSystem{ get; set; }
        public string Nickname { get; set; }
        public string Value { get; set; }
    }
}
