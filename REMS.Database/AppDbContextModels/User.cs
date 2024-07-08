﻿using System;
using System.Collections.Generic;

namespace REMS.Database.AppDbContextModels;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
