using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult BookList()
        {
            return View();
        }

    }
}
