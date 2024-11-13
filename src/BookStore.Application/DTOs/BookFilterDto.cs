namespace BookStore.Application.DTOs
{
    public class BookFilterDto
    {
        public string? Page { get; set; }
        public string? Sort { get; set; }
        public List<string?> Price { get; set; } = new List<string?>();
        public List<string?> Genres { get; set; } = new List<string?>();
        public List<string?> Factory { get; set; } = new List<string?>();
    }
}
