using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class CheckoutDto
    {
        public CartSummaryDto? CartSummary { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string ReceivedName { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string ReceivedPhone { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        public string ReceivedAddress { get; set; } = null!;
        public string? OrderNotes { get; set; }
    }
}
