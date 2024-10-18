using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
