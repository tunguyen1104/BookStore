using BookStore.Application.DTOs.Order;

namespace BookStore.Application.Services
{
    public interface IOrderService
    {
        (IEnumerable<OrderDto> orderDtos, int FilteredCount, int TotalCount) Find(string search, int page, int pageSize);
        Task<List<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(long orderId);
    }
}
