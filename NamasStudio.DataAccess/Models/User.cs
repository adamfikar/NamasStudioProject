using System;
using System.Collections.Generic;

namespace NamasStudio.DataAccess.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public int RoleId { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? RegisterDate { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual RoleUser Role { get; set; } = null!;

    public virtual ICollection<UserAddress> UserAddresses { get; } = new List<UserAddress>();
}
