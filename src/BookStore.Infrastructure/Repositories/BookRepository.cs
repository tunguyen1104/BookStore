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
    }
}