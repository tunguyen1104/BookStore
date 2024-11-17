using BookStore.Domain.Entities;

namespace BookStore.Domain.Repositories
{
    public interface IStockRepository : IRepository<StockImport>
    {
        Task<StockImport?> GetStockImportWithDetailsAsync(long stockImportId);
        IQueryable<StockImport> GetStockImportAsync();
    }
}
