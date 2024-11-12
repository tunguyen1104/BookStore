using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class StockController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ISupplierService _supplierService;
        private readonly IStockService _stockService;

        public StockController(IBookService bookService, ISupplierService supplierService, IStockService stockService)
        {
            _bookService = bookService;
            _supplierService = supplierService;
            _stockService = stockService;
        }

        public IActionResult ListOrder()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetStockImportsAsync(int draw, string searchValue, int start, int length)
        {
            int page = (start / length) + 1;
            var (stocks, filterCount, totalCount) = await _stockService.FindAsync(searchValue, page, length);
            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = stocks
            });
        }

        public async Task<IActionResult> CreateOrderImport()
        {
            ViewBag.Books = await _bookService.GetAllAsync();
            ViewBag.Suppliers = await _supplierService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(StockImportOrderDto model)
        {
            if (ModelState.IsValid)
            {
                await _stockService.CreateImportOrder(model);

                return RedirectToAction("ListOrder");
            }

            // If the model state is invalid, repopulate products and suppliers for re-rendering
            ViewBag.Suppliers = await _supplierService.GetAllAsync();
            ViewBag.Products = await _bookService.GetAllAsync();

            return View(model);
        }
    }
}
