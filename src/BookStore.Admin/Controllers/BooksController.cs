using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult BookList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetProducts(string search, int start, int length)
        {
            var books = _bookService.find(search, start, length);
            var totalCount = books.Count();

            var filteredData = books; // For demonstration purposes
            return Json(new
            {
                draw = Request.Query["draw"],
                recordsTotal = totalCount,
                recordsFiltered = filteredData.Count(),
                data = filteredData
            });
        }
    }
}
