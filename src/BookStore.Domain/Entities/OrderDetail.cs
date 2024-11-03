using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class OrderDetail
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long BookId { get; set; }

    public long Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
