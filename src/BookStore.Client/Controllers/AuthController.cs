using BookStore.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterDTO account)
        {
            return View(account);
        }
    }
}
