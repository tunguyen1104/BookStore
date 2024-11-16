using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public AccountController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
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
    }
}
