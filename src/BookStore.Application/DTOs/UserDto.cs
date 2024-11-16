using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; } = null!;

        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
