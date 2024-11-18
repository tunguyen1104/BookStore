using BookStore.Domain.Entities;

namespace BookStore.Domain.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {

        Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId);
    }
}
