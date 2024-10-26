using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
