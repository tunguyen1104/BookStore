using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public interface IBookService
    {
        Task<Book> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetAllAsync();
        IEnumerable<BookDto> Find(string search, int page, int pageSize);
        IEnumerable<Category> GetAllBookCategories();
    }
}
