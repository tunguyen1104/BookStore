using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Infrastructure.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BookStoreDbContext context) : base(context)
        {
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }
        public async Task<List<User>> GetList(Expression<Func<User, bool>> expresstion)
        {
            return await _context.Users.Where(expresstion).ToListAsync();
        }
    }
}
