using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class StockRepository : Repository<StockImport>, IStockRepository
    {
        public StockRepository(BookStoreDbContext context) : base(context)
        {
        }

        public IQueryable<StockImport> GetStockImportAsync()
        {
            return _context.StockImports.Include(si => si.Supplier).AsQueryable();
        }

        public async Task<StockImport?> GetStockImportWithDetailsAsync(long stockImportId)
        {
            return await _context.StockImports
                .Include(si => si.StockImportDetails)
                    .ThenInclude(sid => sid.Book)
                .Include(si => si.Supplier)
                .FirstOrDefaultAsync(si => si.Id == stockImportId);
        }
    }
}
