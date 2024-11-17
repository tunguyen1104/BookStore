using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult AccountList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAccounts(int draw, string searchValue, int start, int length)
        {
            int page = (start / length) + 1;
            var (accounts, totalCount, filterCount) = _accountService.Find(searchValue, page, length);
            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = accounts
            });
        }
    }
}
