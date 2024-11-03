using BookStore.Domain.Entities;

namespace BookStore.Domain.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> FetchByUserIdAsync(long id);
        int GetSumByUserId(long userId);
    }
}
