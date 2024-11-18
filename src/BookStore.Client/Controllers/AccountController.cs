using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IOrderService _orderService;
        public AccountController(IUserService userService, IFileService fileService, IOrderService orderService)
        {
            _userService = userService;
            _fileService = fileService;
            _orderService = orderService;
        }

        [Route("my-profile")]
        public IActionResult MyProfile()
        {
            var user = _userService.GetCurrentUser();

            var userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Avatar = user.Avatar,
                Address = user.Address,
                Phone = user.Phone,
                Email = user.Email
            };

            return View(userDto);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(userDto);
                TempData["StatusUpdated"] = result;
            }
            userDto.Avatar ??= "/img/avatar/default.png";
            return View("MyProfile", userDto);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var input = new ChangePasswordDto();
            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            TempData["StatusUpdated"] = false;
            if (ModelState.IsValid)
            {
                bool isSuccess = await _userService.ChangePasswordAsync(changePasswordDto);

                if (isSuccess)
                {
                    TempData["StatusUpdated"] = true;
                    return RedirectToAction("ChangePassword");
                }
            }
            return View(changePasswordDto);
        }

        [HttpGet]
        public ActionResult MyPurchase()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMyPurchase(int draw, string searchValue, int start, int length)
        {
            int page = (start / length) + 1;
            var (orderDtos, filterCount, totalCount) =  _orderService.Find(searchValue, page, length);
            return Json(new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = orderDtos
            });
        }


        [HttpGet("Account/GetOrderDetailAsync")]
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
