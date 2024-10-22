using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class StockImport
{
    public long Id { get; set; }

    public long SupplierId { get; set; }

    public DateTime? ImportDate { get; set; }

    public decimal TotalCost { get; set; }

    public virtual ICollection<StockImportDetail> StockImportDetails { get; set; } = new List<StockImportDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
