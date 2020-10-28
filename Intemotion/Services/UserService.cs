using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface IUserService
    {
        ServiceResult<List<User>> FindUsers(string userName, int gameId);
    }

    public class UserService : BaseService, IUserService
    {
        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }
        public ServiceResult<List<User>> FindUsers(string userName, int gameId)
        {
            return Success(context.Users.Where(x => x.UserName.Contains(userName) &&! x.GameUsers.Any(u => u.GameId == gameId)).ToList());
        }
    }
}
