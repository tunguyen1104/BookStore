using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<List<User>> GetList(Expression<Func<User, bool>> expresstion);
    }
}
