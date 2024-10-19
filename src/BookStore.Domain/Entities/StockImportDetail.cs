using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class StockImportDetail
{
    public long Id { get; set; }

    public long StockImportId { get; set; }

    public long BookId { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual StockImport StockImport { get; set; } = null!;
}
