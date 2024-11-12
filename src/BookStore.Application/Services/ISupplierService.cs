using BookStore.Application.DTOs;

namespace BookStore.Application.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
    }
}
