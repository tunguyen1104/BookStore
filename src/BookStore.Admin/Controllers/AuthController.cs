using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.RegisterUserAsync(model))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto account, string? returnUrl = null)
        {
            //ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(account);
            }

            if (await _userService.AuthenticateAndSignIn(account))
            {
                return DetermineRedirectUrl(returnUrl);
            }

            ModelState.AddModelError("Email", "User not found.");
            return View(account);
        }
        private IActionResult DetermineRedirectUrl(string? returnUrl)
        {
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                if (_userService.IsAuthorizedRole(0)) return AccessDenied();
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
