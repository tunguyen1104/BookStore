using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public long EmployeeId { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
