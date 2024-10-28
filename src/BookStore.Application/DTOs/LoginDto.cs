using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class LoginDto
    {
        [RegularExpression(@"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
