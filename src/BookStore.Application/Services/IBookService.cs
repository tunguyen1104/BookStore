using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public interface IBookService
    {
        Task<Book> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetAllAsync();
    }
}
