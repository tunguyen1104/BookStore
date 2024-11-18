using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult OrderManagement()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetOrders(int draw, string searchValue, int start, int length)
        {
            int page = (start / length) + 1;
            var (orderDtos, filterCount, totalCount) = _orderService.Find(searchValue, page, length);
            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = orderDtos
            });
        }
        [HttpGet("Order/GetOrderDetailAsync")]
        public async Task<IActionResult> GetOrderDetailAsync(long orderId)
        {
            var orderDetailDto = await _orderService.GetOrderDetailsByOrderIdAsync(orderId);
            if (orderDetailDto == null || !orderDetailDto.Any())
            {
                return Json(new { success = false, message = "Order details not found" });
            }

            return Json(new { success = true, data = orderDetailDto });
        }
    }
}
