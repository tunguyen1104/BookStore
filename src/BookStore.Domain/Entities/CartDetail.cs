using System;
using System.Collections.Generic;
namespace BookStore.Domain.Entities;

public partial class CartDetail
{
    public long Id { get; set; }

    public long CartId { get; set; }

    public long BookId { get; set; }

    public long Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;
}
