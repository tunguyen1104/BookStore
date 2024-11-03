using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly IBookService _bookService;
        public CartApiController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpPost("add-book-to-cart")]
        public async Task<IActionResult> AddbookToCart([FromBody] CartRequestDto cartRequest)
        {
            if (cartRequest == null || cartRequest.BookId <= 0 || cartRequest.Quantity <= 0)
            {
                return BadRequest("Invalid cart request.");
            }
            await _bookService.AddBookToCartAsync(cartRequest.BookId, cartRequest.Quantity);
            int sum = HttpContext.Session.GetInt32("sum-cart-detail") ?? 0;
            return Ok(sum);
        }
    }
}
