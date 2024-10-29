using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ActionResult GetProducts(int draw, string searchValue, int start, int length)
        {
            int page = (start / length) + 1;
            var (books, filterCount) = _bookService.Find(searchValue, page, length);
            var totalCount = _bookService.getTotal();
            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = books
            });
        }
        public IActionResult CreateBook()
        {
            var categories = _bookService.GetAllBookCategories()
                .Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                });
            ViewBag.Categories = categories;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetBook(long id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            return Json(new
            {
                success = true,
                id = book.Id,
                name = book.Name,
                author = book.Author,
                shortDesc = book.ShortDesc,
                detailDesc = book.DetailDesc,
                price = book.Price,
                quantity = book.Quantity,
                image = book.Image,
                factory = book.Factory,
                discount = book.Discount,
            });
        }

        [HttpPost]
        public async Task<JsonResult> EditBookAsync([FromBody] BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book = await _bookService.GetByIdAsync(bookDto.Id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found" });
                }

                book.Name = bookDto.Name;
                book.Author = bookDto.Author;

                //_bookService.Update(book);

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid data" });
        }
    }
}
