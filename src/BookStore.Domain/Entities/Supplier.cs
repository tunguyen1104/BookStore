using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Supplier
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<StockImport> StockImports { get; set; } = new List<StockImport>();
}
