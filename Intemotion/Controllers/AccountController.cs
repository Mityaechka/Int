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
    public class AccountController : BaseController
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpGet]
        [Route("")]
        public async Task<ControllerResult<UserViewModel>> GetUser()
        {
            return Result((await authService.GetCurrentUser()).Convert(x => new UserViewModel(x)));
        }

    }
}