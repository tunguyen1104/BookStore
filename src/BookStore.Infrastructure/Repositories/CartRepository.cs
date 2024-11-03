using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    internal class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(BookStoreDbContext context) : base(context)
        {
        }
        public async Task<Cart?> FetchByUserIdAsync(long id)
        {
            return await _context.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.User.Id == id);
        }
        public int GetSumByUserId(long userId)
        {
            return _context.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.Sum)
                .FirstOrDefault();
        }
    }
}
