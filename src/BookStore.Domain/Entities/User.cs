namespace BookStore.Domain.Entities;

public partial class User
{
    public long Id { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public bool? IsDeleted { get; set; }

    public long RoleId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public Role Role { get; set; } = null!;
}
