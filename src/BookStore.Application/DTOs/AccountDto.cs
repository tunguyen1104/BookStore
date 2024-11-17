namespace BookStore.Application.DTOs
{
    public class AccountDto
    {
        public long Id { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public bool? IsDeleted { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
