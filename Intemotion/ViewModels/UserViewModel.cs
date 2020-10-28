using Intemotion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User model)
        {
            Id = model.Id;
            Email = model.Email;
            UserName = model.UserName;
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
