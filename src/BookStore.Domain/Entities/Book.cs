using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public partial class Book
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string DetailDesc { get; set; } = null!;

    public decimal Price { get; set; }

    public long Quantity { get; set; }

    public string ShortDesc { get; set; } = null!;

    public long? Sold { get; set; }

    public string Image { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Factory { get; set; } = null!;

    public decimal Discount { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<StockImportDetail> StockImportDetails { get; set; } = new List<StockImportDetail>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}

