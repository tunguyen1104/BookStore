using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Application.Services.Impl
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateImportOrder(StockImportOrderDto request)
        {
            StockImport stockImport = new StockImport();
            stockImport.SupplierId = request.SupplierId;
            stockImport.TotalCost = request.TotalCost;
            var details = request.Products.Select(b => new StockImportDetail
            {
                BookId = b.Id,
                Price = b.UnitPrice,
                Quantity = b.Quantity,
            }).ToList();
            stockImport.StockImportDetails = details;
            _unitOfWork.Stocks.Add(stockImport);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task<(IEnumerable<StockImportOrderDto> stockImportOrders, int FilteredCount, int TotalCount)> FindAsync(string search, int page, int pageSize)
        {
            if (page < 1) page = 1;
            IQueryable<StockImport> stockImportQuery = _unitOfWork.Stocks.GetAll().OrderBy(s => s.ImportDate);
            var totalCount = stockImportQuery.Count();

            if (!string.IsNullOrWhiteSpace(search))
            {
                stockImportQuery = stockImportQuery.Where(stock => (stock.Supplier.Name.Contains(search)));
            }

            var filteredCount = stockImportQuery.Count();

            // Apply paging
            var stockImports = stockImportQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map to BookDto
            var stockDto = stockImports.Select(stock => new StockImportOrderDto
            {
                Id = stock.Id,
                ImportDate = stock.ImportDate ?? DateTime.MinValue,
                SupplierId = stock.SupplierId,
                SupplierName = _unitOfWork.Suppliers.GetByIdAsync(stock.SupplierId).Result.Name,
                TotalCost = stock.TotalCost,
            })
            .ToList();

            return (stockDto, filteredCount, totalCount);
        }
    }
}
