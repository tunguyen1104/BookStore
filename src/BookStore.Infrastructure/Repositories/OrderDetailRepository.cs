using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(BookStoreDbContext context) : base(context)
        {
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Book)
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            return orderDetails;
        }
    }
}
