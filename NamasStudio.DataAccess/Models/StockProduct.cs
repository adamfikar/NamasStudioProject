using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class StockProduct
{
    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public int Stock { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductSize Size { get; set; } = null!;
}
