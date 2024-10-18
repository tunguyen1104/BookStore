using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Customer
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? Points { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
