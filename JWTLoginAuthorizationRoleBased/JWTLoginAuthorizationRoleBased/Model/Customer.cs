using System;
using System.Collections.Generic;

namespace JWTLoginAuthorizationRoleBased.Model;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
