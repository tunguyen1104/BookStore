using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    internal class CartDetailRepository : Repository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(BookStoreDbContext context) : base(context)
        {
        }

        public async Task<CartDetail?> GetCartDetailByCartAndBookAsync(long cartId, long bookId)
        {
            return await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.Cart.Id == cartId && cd.Book.Id == bookId);
        }
    }
}
