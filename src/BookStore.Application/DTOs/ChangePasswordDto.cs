using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string OldPassword { get; set; } = null!;
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string NewPassword { get; set; } = null!;
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [Compare("NewPassword", ErrorMessage = "ConfirmPassword must match the Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
