namespace BookStore.Application.DTOs
{
    public class CartRequestDto
    {
        public long BookId { get; set; }
        public long Quantity { get; set; }
    }
}
