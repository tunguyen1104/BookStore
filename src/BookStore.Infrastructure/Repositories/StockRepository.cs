using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class StockRepository : Repository<StockImport>, IStockRepository
    {
        public StockRepository(BookStoreDbContext context) : base(context)
        {
        }
    }
}
