namespace BookStore.Application.DTOs
{
    public class HomePageBookDto
    {
        public BookDto BookDto { get; set; } = null!;
        public bool IsNewArrival { get; set; }
        public bool BestSeller { get; set; }
    }
}
