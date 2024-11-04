namespace BookStore.Application.DTOs
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
