using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Employee
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Position { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StockImport> StockImports { get; set; } = new List<StockImport>();

    public virtual User? User { get; set; }
}