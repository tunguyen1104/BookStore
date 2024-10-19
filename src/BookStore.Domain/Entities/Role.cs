using System;
using System.Collections.Generic;
namespace BookStore.Domain.Entities;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
