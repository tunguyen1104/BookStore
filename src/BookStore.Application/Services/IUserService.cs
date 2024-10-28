using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public interface IUserService
    {
        Task AddUser(RegisterDto account);
        Task<bool> RegisterUserAsync(RegisterDto account);
        Task<string> HashPassword(string value);
        Task<User?> AuthenticateUserAsync(LoginDto account);
        Task<bool> ValidateHashPassword(string value, string hash);
        Task<bool> AuthenticateAndSignIn(LoginDto account);
        Task LogoutAsync();
        bool IsAuthorizedRole(long roleType);
        User GetCurrentUser();
    }
}
