using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public string? Description { get; set; }

    public decimal Weight { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public string Fabric { get; set; } = null!;

    public string Color { get; set; } = null!;

    public virtual CategoryProduct Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<PhotoProduct> PhotoProducts { get; } = new List<PhotoProduct>();

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();
}
