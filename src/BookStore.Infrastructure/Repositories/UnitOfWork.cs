using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext _context;
        public IBookRepository Books { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public UnitOfWork(BookStoreDbContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Categories = new CategoryRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}