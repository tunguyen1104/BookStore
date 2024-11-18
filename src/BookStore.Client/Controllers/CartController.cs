using BookStore.Application.DTOs;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Client.Controllers
{
    [Authorize]
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
        [HttpPost]
        public async Task<IActionResult> ConfirmCheckout(CartSummaryDto cart)
        {
            if (cart.CartDetailDtos == null)
            {
                return BadRequest("CartDetailDto cannot be null.");
            }
            await _bookService.HandleUpdateCartBeforeCheckout(cart.CartDetailDtos);

            return RedirectToAction("Checkout");
        }
        public async Task<IActionResult> Checkout()
        {
            CartSummaryDto? cartSummary = await _bookService.HandleGetCartPageAsync();
            CheckoutDto? checkout = await _bookService.GetUserCheckoutDataAsync();
            if (checkout == null || cartSummary == null)
            {
                throw new Exception("Cart summary could not be retrieved.");
            }
            checkout.CartSummary = cartSummary;
            return View(checkout);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutDto checkoutDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Checkout", checkoutDto);
            }
            await _bookService.HandleAddOrderAndOrderDetail(checkoutDto.ReceivedName, checkoutDto.ReceivedPhone, checkoutDto.ReceivedAddress, checkoutDto.OrderNotes);
            return RedirectToAction("ThankYou", checkoutDto);
        }

        [HttpGet]
        public IActionResult ThankYou(CheckoutDto checkoutDto)
        {
            return View(checkoutDto);
        }
    }
}
