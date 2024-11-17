using BookStore.Application.DTOs;

namespace BookStore.Application.Services
{
    public interface IAccountService
    {
        (IEnumerable<AccountDto> Accounts, int TotalCount, int FilterCount) Find(string search, int page, int pageSize);
        Task<bool> DisableAccountAsync(long accountId);
    }
}
