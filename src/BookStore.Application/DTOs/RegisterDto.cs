using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class RegisterDto
    {
        [RegularExpression(@"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [Compare("Password", ErrorMessage = "ConfirmPassword must match the Password")]
        public string ConfirmPassword { get; set; }
    }
}
