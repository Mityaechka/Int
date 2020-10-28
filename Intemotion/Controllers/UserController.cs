using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intemotion.Entities;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intemotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        [Route("find")]
        public ControllerResult<List<UserViewModel>> FindUsers(string name,int gameId)
        {
            return Result(userService.FindUsers(name, gameId).Convert(x => x.Select(u => new UserViewModel(u)).ToList()));
        }
    }
}