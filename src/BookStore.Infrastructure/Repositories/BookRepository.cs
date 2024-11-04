using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
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
    }
}