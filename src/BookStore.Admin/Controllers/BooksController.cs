using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Admin.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IFileService _fileService;
        public BooksController(IBookService bookService, IFileService fileService)
        {
            _bookService = bookService;
            _fileService = fileService;
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
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file selected" });
            }

            try
            {
                var imageUrl = await _fileService.UploadImageAsync(file);
                return Json(new { success = true, imageUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDto book)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookService.CreateNewBookAsync(book);
                TempData["SuccessMessage"] = "Book added successfully! Click here to view the book list.";

                return RedirectToAction("CreateBook");
            }
            return View(book);
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
                image = _fileService.GetImageUrl(book.Image),
                factory = book.Factory,
                discount = book.Discount,
            });
        }

        [HttpPost]
        public async Task<JsonResult> EditBook([FromBody] BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book = await _bookService.GetByIdAsync(bookDto.Id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found" });
                }

                var result = _bookService.UpdateBookAsync(bookDto);

                return Json(new { success = result });
            }

            return Json(new { success = false, message = "Invalid data" });
        }
        [HttpPost]
        public async Task<JsonResult> Delete(long bookId)
        {
            var result = await _bookService.DeleteBookAsync(bookId);
            if (result)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Error when delete" });
        }
    }
}
