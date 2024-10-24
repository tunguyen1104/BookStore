using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}