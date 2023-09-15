using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class CategoryProduct
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
    public virtual ICollection<SizeCategory> Sizes { get; } = new List<SizeCategory>();
}
