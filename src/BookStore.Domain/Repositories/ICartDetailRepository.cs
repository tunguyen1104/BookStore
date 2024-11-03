using BookStore.Domain.Entities;

namespace BookStore.Domain.Repositories
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        Task<CartDetail?> GetCartDetailByCartAndBookAsync(long cartId, long bookId);
    }
}
