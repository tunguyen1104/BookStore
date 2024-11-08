using BookStore.Application.DTOs;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class ItemController : Controller
    {
        private readonly IBookService _bookService;

        public ItemController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> GetBookDetail(long id)
        {
            Book? book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            ViewBag.RelatedBooks = await _bookService.GetRelatedBooksAsync(book);
            return View(book);
        }
    }
}
