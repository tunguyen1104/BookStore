namespace BookStore.Application.DTOs
{
    public class ProductImportDto
    {
        public long Id { get; set; }
        public long Quantity { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
