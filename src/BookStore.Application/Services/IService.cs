namespace BookStore.Application.Services
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
