using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
    }
}
