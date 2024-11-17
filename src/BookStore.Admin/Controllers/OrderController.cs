using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
