namespace BookStore.Application.DTOs
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public string DetailDesc { get; set; } = null!;

        public decimal Price { get; set; }

        public long Quantity { get; set; }

        public string ShortDesc { get; set; } = null!;

        public long? Sold { get; set; }

        public string Image { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Factory { get; set; } = null!;

    }
}
