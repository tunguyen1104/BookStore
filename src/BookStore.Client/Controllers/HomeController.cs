using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Book> _bookService;

        public HomeController(ILogger<HomeController> logger, IService<Book> bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
