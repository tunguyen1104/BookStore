using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;
namespace BookStore.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUserAsync(RegisterDto account)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(account.Email.Trim())) return false;
            await AddUser(account);
            return true;
        }

        public async Task AddUser(RegisterDto account)
        {
            var user = _mapper.Map<User>(account);
            user.Password = await HashPassword(account.Password);
            user.RoleId = (long)RoleEnum.USER;
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        public Task<string> HashPassword(string value)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(value, BCrypt.Net.BCrypt.GenerateSalt(12)));
        }
        public async Task<User?> AuthenticateUserAsync(LoginDto account)
        {
            var users = await _unitOfWork.Users.GetList(x => x.Email.Trim() == account.Email.Trim());
            foreach (var user in users)
            {
                if (await ValidateHashPassword(account.Password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<bool> AuthenticateAndSignIn(LoginDto account)
        {
            await LogoutAsync();

            var user = await AuthenticateUserAsync(account);
            if (user == null)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
                new Claim(ClaimTypes.Role, user.RoleId == (long)RoleEnum.ADMIN ? RoleEnum.ADMIN.ToString() : RoleEnum.USER.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = account.RememberMe,
                ExpiresUtc = account.RememberMe ? DateTimeOffset.UtcNow.AddMinutes(60) : DateTimeOffset.UtcNow.AddMinutes(1)
            };

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
            }
            return true;
        }
        public Task<bool> ValidateHashPassword(string value, string hash)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(value, hash));
        }
        public async Task LogoutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
