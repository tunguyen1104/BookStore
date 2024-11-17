namespace BookStore.Application.DTOs.Order
{
    public class OrderDetailDto
    {
        public string BookName { get; set; } = null!;

        public string BookImage { get; set; } = null!;

        public decimal BookPrice { get; set; }

        public long Quantity { get; set; }
    }
}
