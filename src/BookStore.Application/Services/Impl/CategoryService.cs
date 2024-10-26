using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }
    }
}
