using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class RoleUser
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
