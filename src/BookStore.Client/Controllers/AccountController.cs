using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
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

    }
}
