using Intemotion.Entities;
using Intemotion.Models;
using Intemotion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<User>> GetCurrentUser();
        Task<ServiceResult<User>> GetUser(string id);
        Task<ServiceResult> Login(LoginViewModel model);
        Task<ServiceResult> Logout();
        Task<ServiceResult> Register(RegisterViewModel model);
    }

    public class AuthService : BaseService, IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResult<User>> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return Error<User>("Не удалось найти пользователя");
            else
                return Success(user);
        }
        public async Task<ServiceResult<User>> GetCurrentUser()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
                return Success(user);
            else
                return Error<User>("Проверьте введенныу данные");
        }
        public async Task<ServiceResult> Register(RegisterViewModel model)
        {
            if (model.Password != model.PasswordConfirm)
                return Error("Пароли не совпадают");

            User user = new User
            {
                Email = model.Email,
                UserName = model.Username,
                BirthDate = model.BirthDate,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Success();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    return Error(error.Description);
                }
                return Error("Не удалось создать пользователя");
            }
        }
        public async Task<ServiceResult> Login(LoginViewModel model)
        {
            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Success();
            }
            else
            {
                return Error("Неправильный логин и (или) пароль");
            }
        }

        public async Task<ServiceResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Success();
        }
    }
}
