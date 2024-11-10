using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class BookDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string ShortDesc { get; set; } = null!;

        [StringLength(2000)]
        public string DetailDesc { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Range(0, long.MaxValue)]
        public long Quantity { get; set; }

        public long? Sold { get; set; } = 0;

        public string Image { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        public string Factory { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public decimal Discount { get; set; } = 0;
        public bool? IsDeleted { get; set; } = false;

        public List<long> SelectedCategoryIds { get; set; } = new List<long>();
        public IEnumerable<CategoryDto>? Categories { get; set; } = null!;
    }
}
