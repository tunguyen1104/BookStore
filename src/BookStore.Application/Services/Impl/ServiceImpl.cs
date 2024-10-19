using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class ServiceImpl<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public ServiceImpl(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
