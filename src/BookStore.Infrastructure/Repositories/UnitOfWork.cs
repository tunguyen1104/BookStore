using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext _context;
        public IBookRepository Books { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IUserRepository Users { get; private set; }
        public ICartRepository Carts { get; private set; }
        public ICartDetailRepository CartDetails { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        public UnitOfWork(BookStoreDbContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Categories = new CategoryRepository(_context);
            Users = new UserRepository(_context);
            Carts = new CartRepository(_context);
            CartDetails = new CartDetailRepository(_context);
            Orders = new OrderRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
