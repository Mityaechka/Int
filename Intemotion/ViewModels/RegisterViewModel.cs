using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public enum Gender
    {
        M,
        F
    }
}
