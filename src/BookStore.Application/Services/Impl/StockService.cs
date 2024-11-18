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
                StockImport = stockImport,
            }).ToList();
            stockImport.StockImportDetails = details;
            _unitOfWork.Stocks.Add(stockImport);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public (IEnumerable<StockImportOrderDto> stockImportOrders, int FilteredCount, int TotalCount) FindAsync(string search, int page, int pageSize)
        {
            if (page < 1) page = 1;
            IQueryable<StockImport> stockImportQuery = _unitOfWork.Stocks.GetStockImportAsync().OrderBy(s => s.ImportDate);
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
                SupplierName = stock.Supplier.Name,
                TotalCost = stock.TotalCost,
            })
            .ToList();

            return (stockDto, filteredCount, totalCount);
        }

        public async Task<StockImportOrderDto?> GetImportDetails(long stockImportId)
        {
            var order = await _unitOfWork.Stocks.GetStockImportWithDetailsAsync(stockImportId);
            if (order != null)
            {
                var bookImports = order.StockImportDetails.Select(sid => new ProductImportDto
                {
                    Id = sid.BookId,
                    Quantity = sid.Quantity,
                    UnitPrice = sid.Price,
                    Name = sid.Book.Name,
                }
                );
                return new StockImportOrderDto
                {
                    Id = order.Id,
                    ImportDate = order.ImportDate ?? DateTime.MinValue,
                    SupplierId = order.Supplier.Id,
                    SupplierName = order.Supplier.Name,
                    Products = bookImports,
                    TotalCost = order.TotalCost,
                };
            }
            return null;
        }
    }
}
