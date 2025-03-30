using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!await _authService.LoginAsync(model))
            {
                ModelState.AddModelError("", "Неверный логин или пароль!");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }    

            if (!await _authService.RegisterAsync(model))
            {
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                return View(model);
            }

            return RedirectToAction("Login");
        }
    }
}
