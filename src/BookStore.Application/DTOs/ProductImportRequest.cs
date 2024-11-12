namespace BookStore.Application.DTOs
{
    public class ProductImportRequest
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
