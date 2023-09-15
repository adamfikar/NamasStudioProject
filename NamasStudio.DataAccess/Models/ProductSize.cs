using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class ProductSize
{
    public int SizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public string? Waist { get; set; }

    public string? Hips { get; set; }

    public string? LengthLower { get; set; }

    public string? Bust { get; set; }

    public string? LengthUpper { get; set; }

    public string? ArmHole { get; set; }

    public string? BottomSleeve { get; set; }

    public string? SleeveLength { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<StockProduct> StockProducts { get; } = new List<StockProduct>();

    public virtual ICollection<SizeCategory> Sizes { get; } = new List<SizeCategory>();
}
