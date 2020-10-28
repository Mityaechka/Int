using Intemotion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.ViewModels
{
    public class VoucherViewModel
    {
        public VoucherViewModel(Voucher model)
        {
            Id = model.Id;
            if (model.UserId != null)
                User = new UserViewModel(model.User);
            Game = new GameViewModel(model.Game);
            Code = model.Code;
            IsActive = model.IsActive;
        }
        public int Id { get; set; }
        public UserViewModel User { get; set; }
        public GameViewModel Game { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
