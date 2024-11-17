using BookStore.Application.DTOs;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DisableAccountAsync(long accountId)
        {
            var user = _unitOfWork.Users.Find(a => a.Id == accountId).FirstOrDefault();
            if (user != null)
            {
                user.IsDeleted = true;
                _unitOfWork.Users.Update(user);
                return await _unitOfWork.CompleteAsync() > 0;
            }
            return false;
        }

        public (IEnumerable<AccountDto> Accounts, int TotalCount, int FilterCount) Find(string search, int page, int pageSize)
        {
            var accountQuery = _unitOfWork.Users.GetAll().OrderBy(a => a.Id).Where(a => !a.IsDeleted.HasValue || a.IsDeleted.Value);
            var totalCount = accountQuery.Count();
            if (!string.IsNullOrEmpty(search))
            {
                accountQuery.Where(a => a.FullName!.Contains(search) || a.Email.Contains(search) || a.Phone!.Contains(search));
            }
            var filterCount = accountQuery.Count();
            var accountsPaged = accountQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var accounts = accountsPaged.Select(acc => new AccountDto
            {
                Id = acc.Id,
                Address = acc.Address,
                Email = acc.Email,
                FullName = acc.FullName,
                IsDeleted = acc.IsDeleted,
                Phone = acc.Phone,

            });

            return (accounts, totalCount, filterCount);
        }
    }
}
