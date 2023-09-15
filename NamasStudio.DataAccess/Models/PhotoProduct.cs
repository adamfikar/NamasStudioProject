using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class PhotoProduct
{
    public int PhotoId { get; set; }

    public int ProductId { get; set; }

    public string PathName { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
