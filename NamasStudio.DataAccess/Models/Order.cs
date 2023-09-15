using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Username { get; set; } = null!;

    public string ShipCode { get; set; } = null!;

    public decimal ShipCost { get; set; }

    public DateTime OrderDate { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
