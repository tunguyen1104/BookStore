using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(long id);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
