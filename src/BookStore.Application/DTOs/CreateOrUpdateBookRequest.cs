using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
	public class CreateOrUpdateBookRequest
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

		public string Image { get; set; } = null!;

		[Required]
		public string Author { get; set; } = null!;

		public decimal Discount { get; set; } = 0;

		public string Factory { get; set; } = null!;
		public bool? IsDeleted { get; set; } = false;

		public List<long> SelectedCategoryIds { get; set; } = new List<long>();
	}
}
