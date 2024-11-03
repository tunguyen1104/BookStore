namespace BookStore.Application.DTOs
{
    public class CartDetailDto
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public decimal BookPrice { get; set; }
        public long Quantity { get; set; }
        public string BookName { get; set; } = null!;
        public string BookImage { get; set; } = null!;
        public decimal BookDiscount { get; set; }
    }
}
