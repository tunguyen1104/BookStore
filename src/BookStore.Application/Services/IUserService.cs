using BookStore.Application.DTOs;

namespace BookStore.Application.Services
{
    public interface IUserService
    {
        Task AddUser(RegisterDto account);
        Task<bool> RegisterUserAsync(RegisterDto account);
    }
}
