using BookStore.Application.DTOs;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            return (await _unitOfWork.Suppliers.GetAllAsync())
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    ContactEmail = s.ContactEmail,
                    ContactPhone = s.ContactPhone,
                })
                .ToList();
        }
    }
}
