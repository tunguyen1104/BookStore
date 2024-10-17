using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class ServiceImpl : IService
    {
        private readonly IRepository _repository;
        public ServiceImpl(IRepository repository)
        {
            _repository = repository;
        }
    }
}
