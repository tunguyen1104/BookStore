using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class CartController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
