namespace BookStore.Application.DTOs.Order
{
    public class OrderDto
    {
        public long OrderId { get; set; }

        public string ReceivedName { get; set; } = null!;

        public string ReceivedPhone { get; set; } = null!;

        public string ReceivedAddress { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }

        public string OrderNotes { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
