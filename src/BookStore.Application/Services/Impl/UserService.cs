using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using BookStore.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
namespace BookStore.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionService _sessionService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _sessionService = sessionService;
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
                new Claim(ClaimTypes.Role, IsAuthorizedRole(user.RoleId) ? RoleEnum.ADMIN.ToString() : RoleEnum.USER.ToString())
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
                httpContext.Session.Clear();
                httpContext.Response.Cookies.Delete(".AspNetCore.Session");
            }
        }
        public bool IsAuthorizedRole(long roleType)
        {
            if (roleType == 0)
            {
                return _httpContextAccessor.HttpContext.User.IsInRole(RoleEnum.ADMIN.ToString());
            }
            return roleType == (long)RoleEnum.ADMIN;
        }
        public User GetCurrentUser()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userDataClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            var user = string.IsNullOrEmpty(userDataClaim)
                ? null
                : JsonConvert.DeserializeObject<User>(userDataClaim);

            return new User
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Avatar = user?.Avatar ?? "/img/avatar/default.png",
                Address = user?.Address,
                Phone = user?.Phone
            };
        }
        public int SetCartDetailTotalInSession(User user)
        {
            int totalQuantity = _unitOfWork.Carts.GetSumByUserId(user.Id);
            _sessionService.UpdateCartSum(totalQuantity);
            return totalQuantity;
        }

        private async Task CreateAndSignInClaimsAsync(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
                new Claim(ClaimTypes.Role, IsAuthorizedRole(user.RoleId) ? RoleEnum.ADMIN.ToString() : RoleEnum.USER.ToString()),
                new Claim("RememberMe", rememberMe.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddMinutes(60) : DateTimeOffset.UtcNow.AddMinutes(1)
            };

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
            }
        }

        private async Task UpdateClaimsAndSignInAsync(User user)
        {
            await CreateAndSignInClaimsAsync(user, true);
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                return false;
            }

            _mapper.Map(userDto, user);
            _unitOfWork.Users.Update(user);

            //save
            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                return false;
            }

            await UpdateClaimsAndSignInAsync(user);

            return true;
        }
    }
}
