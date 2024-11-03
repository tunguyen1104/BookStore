using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;
        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> ShoppingCart()
        {
            var cartSummary = await _bookService.HandleGetCartPageAsync();

            if (cartSummary == null)
            {
                return NotFound("Cart summary could not be retrieved.");
            }

            return View(cartSummary);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCartDetail(long cartDetailId)
        {
            await _bookService.HandleRemoveCartDetail(cartDetailId);
            return RedirectToAction("ShoppingCart");
        }
    }
}
