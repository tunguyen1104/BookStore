using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class StockImportOrderDto
    {
        public long Id { get; set; }
        [Required]
        public long SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        public DateTime ImportDate { get; set; } = DateTime.Now;

        public IEnumerable<ProductImportDto> Products { get; set; } = Enumerable.Empty<ProductImportDto>();

        public decimal TotalCost { get; set; } = 0;
    }
}
