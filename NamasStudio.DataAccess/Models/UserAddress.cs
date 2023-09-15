using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class UserAddress
{
    public int AddressId { get; set; }

    public string Username { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
