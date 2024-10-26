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
    }
}
