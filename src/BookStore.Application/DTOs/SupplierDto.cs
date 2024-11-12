namespace BookStore.Application.DTOs
{
    public class SupplierDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? Address { get; set; }
    }
}
