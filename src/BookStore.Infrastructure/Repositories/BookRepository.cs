using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Infrastructure.Repositories
{
    internal class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context) : base(context)
        {
        }

        public int count()
        {
            return _context.Books.AsQueryable().Where(b => !b.IsDeleted.HasValue || !b.IsDeleted.Value).Count();
        }

        public IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate, int pageNumber, int pageSize)
        {
            return _context.Books.Where(predicate)
                 .Skip(pageSize * (pageNumber))
                 .Take(pageSize);
        }
        public async Task<decimal?> FetchBookDiscountByIdAsync(long bookId) => bookId > 0 ? (await _context.Books.FindAsync(bookId))?.Discount : null;

        public async Task<List<long>> GetNewArrivalBookIdsAsync()
        {
            return await _context.Books
                .OrderByDescending(b => b.Id)
                .Take(20)
                .Select(b => b.Id)
                .ToListAsync();
        }

        public async Task<List<long>> GetMostBuyBookIdsAsync()
        {
            return await _context.Books
                .Where(b => b.Sold.HasValue && b.Sold > 0)
                .OrderByDescending(b => b.Sold)
                .Take(20)
                .Select(b => b.Id)
                .ToListAsync();
        }
    }
}