using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intemotion.Models;
using Intemotion.Services;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intemotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ObjectControllerResult> Register(RegisterViewModel model)
        {
            return Result(await authService.Register(model));
        }
        [HttpPost]
        [Route("login")]
        public async Task<ObjectControllerResult> Login(LoginViewModel model)
        {
            return Result(await authService.Login(model));
        }
        [HttpGet]
        [Route("logout")]
        public async Task<ObjectControllerResult> Logout()
        {
            var result = await authService.Logout();
            return Result(result);
        }
    }
}
